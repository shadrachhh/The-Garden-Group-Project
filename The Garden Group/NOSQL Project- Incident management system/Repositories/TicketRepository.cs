using MongoDB.Driver;
using NOSQL_Project__Incident_management_system.Models;
using NOSQL_Project__Incident_management_system.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NOSQL_Project__Incident_management_system.Repositories
{
    public class TicketRepository
    {
        private readonly MongoDbContext _context;

        public TicketRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<List<Ticket>> GetAllAsync() =>
            await _context.Tickets.Find(_ => true).ToListAsync();

        public async Task<Ticket> GetByIdAsync(string id) =>
            await _context.Tickets.Find(t => t.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Ticket ticket) =>
            await _context.Tickets.InsertOneAsync(ticket);

        public async Task UpdateAsync(string id, Ticket ticket) =>
            await _context.Tickets.ReplaceOneAsync(t => t.Id == id, ticket);

        public async Task DeleteAsync(string id) =>
            await _context.Tickets.DeleteOneAsync(t => t.Id == id);
    }
}
