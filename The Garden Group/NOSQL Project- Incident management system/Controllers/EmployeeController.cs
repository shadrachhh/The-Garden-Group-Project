using Microsoft.AspNetCore.Mvc;
using NOSQL_Project__Incident_management_system.Models;
using NOSQL_Project__Incident_management_system.Repositories;

namespace NOSQL_Project__Incident_management_system.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeRepository _employeeRepo;

        public EmployeeController(EmployeeRepository employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _employeeRepo.GetAllAsync();
            return View(employees);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                await _employeeRepo.CreateAsync(employee);
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var employee = await _employeeRepo.GetByIdAsync(id);
            if (employee == null)
                return NotFound();

            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, Employee employee)
        {
            if (ModelState.IsValid)
            {
                await _employeeRepo.UpdateAsync(id, employee);
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var employee = await _employeeRepo.GetByIdAsync(id);
            if (employee == null)
                return NotFound();

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _employeeRepo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
