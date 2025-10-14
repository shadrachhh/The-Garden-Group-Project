using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace NOSQL_Project__Incident_management_system.Controllers
{
    public class TestController : Controller
    {
        private readonly IMongoDatabase _db;

        public TestController(IMongoDatabase db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var collections = await _db.ListCollectionNames().ToListAsync();
            return Content($"Connected to MongoDB! Collections: {string.Join(", ", collections)}");
        }
    }
}
