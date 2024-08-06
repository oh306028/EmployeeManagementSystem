using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ManagmentApp.Models
{
    public class Compensation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public double SalaryPerMonth { get; set; }

        public double BonusPerMonth { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string EmployeeId { get; set; }  
    }
}
