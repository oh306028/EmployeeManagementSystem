using ManagmentApp.DbStorage;
using ManagmentApp.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ManagmentApp.Repositories
{
    public class DepartamentRepo
    {
        private readonly IMongoCollection<Departament> _employeeCollection;
        private const string COLLECTION_NAME = "Departaments";  
        public DepartamentRepo(IOptions<EmployeeSystemSettings> options)
        {   
            var mongoClient = new MongoClient(
            options.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                options.Value.DataBaseName);

            _employeeCollection = mongoDatabase.GetCollection<Departament>(COLLECTION_NAME);

        }
        public async Task<List<Departament>> GetAllAsync() =>
        await _employeeCollection.Find(_ => true).ToListAsync();

        public async Task<Departament?> GetAsync(string id) =>
            await _employeeCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Departament newDepartament) =>
            await _employeeCollection.InsertOneAsync(newDepartament);   

        public async Task UpdateAsync(string id, Departament updateDepartament) =>  
            await _employeeCollection.ReplaceOneAsync(x => x.Id == id, updateDepartament);
            
        public async Task RemoveAsync(string id) =>
            await _employeeCollection.DeleteOneAsync(x => x.Id == id);

    }
}
