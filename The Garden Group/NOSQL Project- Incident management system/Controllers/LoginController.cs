using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NOSQL_Project__Incident_management_system.Repositories;

namespace NOSQL_Project__Incident_management_system.Controllers
{
    public class LoginController : Controller
    {
        private readonly EmployeeRepository _employeeRepo;

        public LoginController(EmployeeRepository employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(); // simple login form with Email input
        }

        [HttpPost]
        public async Task<IActionResult> Index(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                ModelState.AddModelError("", "Please enter your email.");
                return View();
            }

            var employee = await _employeeRepo.GetByEmailAsync(email);
            if (employee == null)
            {
                ModelState.AddModelError("", "No employee found with that email.");
                return View();
            }

            // Save login info in session
            HttpContext.Session.SetString("EmployeeId", employee.Id);
            HttpContext.Session.SetString("Role", employee.Role ?? "employee");
            HttpContext.Session.SetString("EmployeeName", employee.Name ?? "");

            return RedirectToAction("Index", "Tickets");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
