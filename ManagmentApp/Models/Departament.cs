using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ManagmentApp.Models
{
    public class Departament
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }

    }
}
