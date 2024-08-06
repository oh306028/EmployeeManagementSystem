using ManagmentApp.DbStorage;
using ManagmentApp.Dtos;
using ManagmentApp.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ManagmentApp.Repositories
{
    public class DepartamentRepo
    {
        private readonly IMongoCollection<Employee> _employeeCollection;
        private readonly IMongoCollection<Departament> _departamentCollection;  
        private const string COLLECTION_NAME = "Departaments";  
        public DepartamentRepo(IOptions<EmployeeSystemSettings> options)
        {   
            var mongoClient = new MongoClient(
            options.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                options.Value.DataBaseName);

            _employeeCollection = mongoDatabase.GetCollection<Employee>("Employees");       
            _departamentCollection = mongoDatabase.GetCollection<Departament>(COLLECTION_NAME);

        }
        public async Task<List<Departament>> GetAllAsync() =>
        await _departamentCollection.Find(_ => true).ToListAsync();

        public async Task<Departament?> GetAsync(string id) =>
            await _departamentCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Departament newDepartament) =>
            await _departamentCollection.InsertOneAsync(newDepartament);   

        public async Task UpdateAsync(string id, Departament updateDepartament) =>  
            await _departamentCollection.ReplaceOneAsync(x => x.Id == id, updateDepartament);
            
        public async Task RemoveAsync(string id) =>
            await _departamentCollection.DeleteOneAsync(x => x.Id == id);   

        public async Task<List<DepartamentDto>> GetEmployeesForEachDepartament()
        {
            var departmentsBson = await _departamentCollection.Find(_ => true).ToListAsync();
            var employeesBson = await _employeeCollection.Find(_ => true).ToListAsync();

            var departmentsDto = departmentsBson.Select(d => new DepartamentDto
            {
                Id = d.Id.ToString(),
                Name = d.Name.ToString(),
                Employees = employeesBson
                    .Where(e => e.DepartamentId.ToString() == d.Id.ToString())
                    .Select(e => new EmployeeDto
                    {
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        Email = e.Email,


                    })
                    .ToList()
            }).ToList();

            return departmentsDto;

        }

    }
}
