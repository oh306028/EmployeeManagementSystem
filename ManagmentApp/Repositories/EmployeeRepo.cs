using ManagmentApp.DbStorage;
using ManagmentApp.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ManagmentApp.Repositories
{
    public class EmployeeRepo
    {
        private readonly IMongoCollection<Employee> _employeeCollection;
        public EmployeeRepo(IOptions<EmployeeSystemSettings> options)
        {
            
        }
    }
}
