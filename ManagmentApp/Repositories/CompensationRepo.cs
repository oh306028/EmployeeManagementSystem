using ManagmentApp.DbStorage;
using ManagmentApp.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ManagmentApp.Repositories
{
    public class CompensationRepo
    {    
            private readonly IMongoCollection<Compensation> _compensationCollection;
        private readonly EmployeeRepo _employeeRepo;    
        private const string COLLECTION_NAME = "Compensations";
            public CompensationRepo(IOptions<EmployeeSystemSettings> options, EmployeeRepo employeeRepo)   
            {
                var mongoClient = new MongoClient(
                options.Value.ConnectionString);

                var mongoDatabase = mongoClient.GetDatabase(
                    options.Value.DataBaseName);

                _compensationCollection = mongoDatabase.GetCollection<Compensation>(COLLECTION_NAME);

                _employeeRepo = employeeRepo;
        }
            public async Task<List<Compensation>> GetAllAsync() =>
            await _compensationCollection.Find(_ => true).ToListAsync();

            public async Task<Compensation?> GetAsync(string employeeName)
            {
                var employee = await _employeeRepo.GetByName(employeeName);

                var result = await _compensationCollection.Find(x => x.EmployeeId == employee.Id).FirstOrDefaultAsync();

                return result;
            } 
                

            public async Task CreateAsync(Compensation newSalary) =>    
                await _compensationCollection.InsertOneAsync(newSalary);    

            public async Task UpdateAsync(string id, Compensation updateSalary) =>
                await _compensationCollection.ReplaceOneAsync(x => x.Id == id, updateSalary);

            public async Task RemoveAsync(string id) => 
                await _compensationCollection.DeleteOneAsync(x => x.Id == id);

        }
    }
