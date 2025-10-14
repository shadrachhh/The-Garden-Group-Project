using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using NOSQL_Project__Incident_management_system.Models;

namespace NOSQL_Project__Incident_management_system.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var connectionString = configuration["MONGODB_URI"];
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase("terkumaDB");
        }

        public IMongoCollection<Employee> Employees => _database.GetCollection<Employee>("employees");
        public IMongoCollection<Ticket> Tickets => _database.GetCollection<Ticket>("tickets");
    }
}
