using ManagmentApp.DbStorage;
using ManagmentApp.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ManagmentApp.Repositories
{
    public class EmployeeRepo
    {
        private readonly IMongoCollection<Employee> _employeeCollection;
        private const string COLLECTION_NAME = "Employees";
        public EmployeeRepo(IOptions<EmployeeSystemSettings> options)
        {
            var mongoClient = new MongoClient(
            options.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                options.Value.DataBaseName);

            _employeeCollection = mongoDatabase.GetCollection<Employee>(COLLECTION_NAME);
             
        }
        public async Task<List<Employee>> GetAllAsync() =>
        await _employeeCollection.Find(_ => true).ToListAsync();
            
        public async Task<Employee?> GetAsync(string id) =>
            await _employeeCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Employee newEmp) =>   
            await _employeeCollection.InsertOneAsync(newEmp);
            
        public async Task UpdateAsync(string id, Employee updateEmp) =>
            await _employeeCollection.ReplaceOneAsync(x => x.Id == id, updateEmp);

        public async Task RemoveAsync(string id) =>
            await _employeeCollection.DeleteOneAsync(x => x.Id == id);


        public async Task<List<EmployeeWithDetails>> GetEmployeeWithCompensation() 
        {
                var pipeline = new[]    
            {
                    new BsonDocument("$lookup", new BsonDocument
                {
                    { "from", "Compensations" },
                    { "localField", "_id" },
                    { "foreignField", "EmployeeId" },
                    { "as", "CompensationDetails" }
                }),
                new BsonDocument("$unwind", "$CompensationDetails"),
                new BsonDocument("$lookup", new BsonDocument
                {
                    { "from", "Departaments" },
                    { "localField", "DepartamentId" },
                    { "foreignField", "_id" },
                    { "as", "DepartamentDetails" }
                }),
                new BsonDocument("$unwind", "$DepartamentDetails"),
                new BsonDocument("$project", new BsonDocument
                {
                    { "Id", "$_id" },
                    { "FirstName", "$FirstName" },
                    { "LastName", "$LastName" },
                    { "HireDate", "$HireDate" },
                    { "DepartamentName", "$DepartamentDetails.Name" },
                    { "Email", "$Email" },
                    { "SalaryPerMonth", "$CompensationDetails.SalaryPerMonth" },
                    { "BonusPerMonth", "$CompensationDetails.BonusPerMonth" }
                })
            };

             var results = await _employeeCollection.Aggregate<BsonDocument>(pipeline).ToListAsync();   

            var employeesWithCompensation = new List<EmployeeWithDetails>();   

            foreach (var result in results)
            {
                employeesWithCompensation.Add(new EmployeeWithDetails
                {
                    Id = result["_id"].AsObjectId.ToString(),
                    FirstName = result["FirstName"].AsString,
                    LastName = result["LastName"].AsString,
                    HireDate = result["HireDate"].ToUniversalTime(),
                    DepartamentName = result["DepartamentName"].AsString,
                    Email = result["Email"].AsString,
                    SalaryPerMonth = result["SalaryPerMonth"].ToDouble(),
                    BonusPerMonth = result["BonusPerMonth"].ToDouble(),
                    
                });
            }

            return employeesWithCompensation;

        }



    }
}
