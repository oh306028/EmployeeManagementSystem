using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ManagmentApp.Dtos
{
    public class DepartamentDto
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        public List<EmployeeDto> Employees{ get; set; } 
    }
}
