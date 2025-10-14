using Microsoft.AspNetCore.Mvc;
using NOSQL_Project__Incident_management_system.Models;
using NOSQL_Project__Incident_management_system.Repositories;
using System.Threading.Tasks;

namespace NOSQL_Project__Incident_management_system.Controllers
{
    public class TicketsController : Controller
    {
        private readonly TicketRepository _ticketRepo;

        public TicketsController(TicketRepository ticketRepo)
        {
            _ticketRepo = ticketRepo;
        }

        public async Task<IActionResult> Index()
        {
            var tickets = await _ticketRepo.GetAllAsync();
            return View(tickets);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Ticket ticket)
        {
            await _ticketRepo.CreateAsync(ticket);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            var ticket = await _ticketRepo.GetByIdAsync(id);
            return View(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, Ticket ticket)
        {
            await _ticketRepo.UpdateAsync(id, ticket);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _ticketRepo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
