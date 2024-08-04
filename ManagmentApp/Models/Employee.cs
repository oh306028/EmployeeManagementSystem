using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ManagmentApp.Models
{
    public class Employee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime HireDate { get; set; }
        public string Email { get; set; }   
    }
}
