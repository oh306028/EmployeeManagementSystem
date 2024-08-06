using ManagmentApp.DbStorage;
using ManagmentApp.Dtos;
using ManagmentApp.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ManagmentApp.Repositories
{
    public class DepartmentRepo
    {
        private readonly IMongoCollection<Employee> _employeeCollection;
        private readonly IMongoCollection<Department> _DepartmentCollection;  
        private const string COLLECTION_NAME = "Departments";  
        public DepartmentRepo(IOptions<EmployeeSystemSettings> options)
        {   
            var mongoClient = new MongoClient(
            options.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                options.Value.DataBaseName);

            _employeeCollection = mongoDatabase.GetCollection<Employee>("Employees");       
            _DepartmentCollection = mongoDatabase.GetCollection<Department>(COLLECTION_NAME);

        }
        public async Task<List<Department>> GetAllAsync() =>
        await _DepartmentCollection.Find(_ => true).ToListAsync();

        public async Task<Department?> GetAsync(string id) =>
            await _DepartmentCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<Department?> GetByNameAsync(string name) =>       
          await _DepartmentCollection.Find(x => x.Name.ToLower() == name.ToLower()).FirstOrDefaultAsync();   

        public async Task CreateAsync(Department newDepartment) =>
            await _DepartmentCollection.InsertOneAsync(newDepartment);   

        public async Task UpdateAsync(string id, Department updateDepartment) =>  
            await _DepartmentCollection.ReplaceOneAsync(x => x.Id == id, updateDepartment);
            
        public async Task RemoveAsync(string id) =>
            await _DepartmentCollection.DeleteOneAsync(x => x.Id == id);   



        public async Task<List<DepartmentDto>> GetEmployeesForEachDepartment()
        {
            var departmentsBson = await _DepartmentCollection.Find(_ => true).ToListAsync();
            var employeesBson = await _employeeCollection.Find(_ => true).ToListAsync();

            var departmentsDto = departmentsBson.Select(d => new DepartmentDto
            {
                Id = d.Id.ToString(),
                Name = d.Name.ToString(),
                Employees = employeesBson
                    .Where(e => e.DepartmentId.ToString() == d.Id.ToString())
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
