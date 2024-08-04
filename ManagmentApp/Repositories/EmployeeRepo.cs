using ManagmentApp.DbStorage;
using ManagmentApp.Models;
using Microsoft.Extensions.Options;
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

    }
}
