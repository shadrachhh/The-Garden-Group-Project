using MongoDB.Driver;
using NOSQL_Project__Incident_management_system.Data;
using NOSQL_Project__Incident_management_system.Models;

namespace NOSQL_Project__Incident_management_system.Repositories
{
    public class EmployeeRepository
    {
        private readonly MongoDbContext _context;

        public EmployeeRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _context.Employees.Find(_ => true).ToListAsync();
        }

        public async Task<Employee> GetByIdAsync(string id)
        {
            return await _context.Employees.Find(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Employee employee)
        {
            await _context.Employees.InsertOneAsync(employee);
        }

        public async Task UpdateAsync(string id, Employee employee)
        {
            await _context.Employees.ReplaceOneAsync(e => e.Id == id, employee);
        }

        public async Task DeleteAsync(string id)
        {
            await _context.Employees.DeleteOneAsync(e => e.Id == id);
        }
    }
}
