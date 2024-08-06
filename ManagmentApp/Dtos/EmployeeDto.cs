using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ManagmentApp.Dtos
{
    public class EmployeeDto
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

    }
}
