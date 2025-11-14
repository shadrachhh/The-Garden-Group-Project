using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NOSQL_Project__Incident_management_system.Models
{
    [BsonIgnoreExtraElements]  
    public class Employee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("department")]
        public string Department { get; set; }

        [BsonElement("role")]
        public string Role { get; set; } 
    }
}
