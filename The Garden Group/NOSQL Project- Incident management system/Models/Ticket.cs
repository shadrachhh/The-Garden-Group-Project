using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace NOSQL_Project__Incident_management_system.Models
{
    public class Ticket
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("employeeId")]
        public string EmployeeId { get; set; }


        [BsonElement("subject")]
        public string Subject { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("priority")]
        public string Priority { get; set; } // Low, Medium, High, Urgent

        [BsonElement("status")]
        public TicketStatus Status { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [BsonElement("updatedAt")]
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;

        [BsonElement("resolvedAt")]
        public DateTime? ResolvedAt { get; set; }

        [BsonElement("tags")]
        public List<string> Tags { get; set; } = new List<string>();


        [BsonElement("statusHistory")]
        public List<StatusHistoryItem> StatusHistory { get; set; } = new List<StatusHistoryItem>();

        public class StatusHistoryItem
        {
            [BsonElement("status")]
            public string Status { get; set; }

            [BsonElement("at")]
            public DateTime At { get; set; }
        }




    }
}
