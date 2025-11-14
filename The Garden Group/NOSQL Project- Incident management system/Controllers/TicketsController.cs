using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NOSQL_Project__Incident_management_system.Models;
using NOSQL_Project__Incident_management_system.Repositories;
using NOSQL_Project__Incident_management_system.Services;



namespace NOSQL_Project__Incident_management_system.Controllers
{
    public class TicketsController : Controller
    {
        private readonly TicketRepository _ticketRepo;
        private readonly TicketSorter _ticketSorter;

        public TicketsController(TicketRepository ticketRepo, TicketSorter ticketSorter)
        {
            _ticketRepo = ticketRepo;
            _ticketSorter = ticketSorter;
        }


        public async Task<IActionResult> Index()
        {
            var role = HttpContext.Session.GetString("Role");
            var employeeId = HttpContext.Session.GetString("EmployeeId");

            if (string.IsNullOrEmpty(role) || string.IsNullOrEmpty(employeeId))
            {
                return RedirectToAction("Index", "Login");
            }

            List<Ticket> tickets;

            if (role == "servicedesk")
            {

                tickets = await _ticketRepo.GetAllAsync();
            }
            else
            {

                tickets = await _ticketRepo.GetByEmployeeIdAsync(employeeId);
            }

            tickets = _ticketSorter.SortByPriority(tickets);


            return View(tickets);
        }


        public async Task<IActionResult> Active()
        {
            var role = HttpContext.Session.GetString("Role");
            var employeeId = HttpContext.Session.GetString("EmployeeId");

            if (string.IsNullOrEmpty(role) || string.IsNullOrEmpty(employeeId))
            {
                return RedirectToAction("Index", "Login");
            }

            List<Ticket> tickets;

            if (role == "servicedesk")
            {
                // All active 
                tickets = await _ticketRepo.GetActiveAsync();
            }
            else
            {
                // Only active tickets for the logged-in employee
                tickets = await _ticketRepo.GetActiveByEmployeeIdAsync(employeeId);
            }


            tickets = _ticketSorter.SortByPriority(tickets);


            return View("Index", tickets);
        }


        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Ticket ticket)
        {
            var employeeId = HttpContext.Session.GetString("EmployeeId");
            if (string.IsNullOrEmpty(employeeId))
            {
                return RedirectToAction("Index", "Login");
            }

            if (!ModelState.IsValid)
            {
                return View(ticket);
            }

            // Force ticket to belong to the logged-in emp
            ticket.EmployeeId = employeeId;
            ticket.Status = TicketStatus.Open;
            ticket.CreatedAt = DateTime.Now;
            ticket.UpdatedAt = DateTime.Now;

            await _ticketRepo.CreateAsync(ticket);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            var ticket = await _ticketRepo.GetByIdAsync(id);
            if (ticket == null) return NotFound();

            return View(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                return View(ticket);
            }

            ticket.UpdatedAt = DateTime.Now;

            await _ticketRepo.UpdateAsync(id, ticket);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            var ticket = await _ticketRepo.GetByIdAsync(id);
            if (ticket == null) return NotFound();

            return View(ticket);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _ticketRepo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Dashboard()
        {
            var role = HttpContext.Session.GetString("Role");
            var employeeId = HttpContext.Session.GetString("EmployeeId");

            if (string.IsNullOrEmpty(role) || string.IsNullOrEmpty(employeeId))
            {
                return RedirectToAction("Index", "Login");
            }

            List<Ticket> tickets;

            if (role == "servicedesk")
            {
                tickets = await _ticketRepo.GetAllForDashboardAsync();
            }
            else
            {
                tickets = await _ticketRepo.GetByEmployeeIdAsync(employeeId);
            }

            var total = tickets.Count;
            if (total == 0) total = 1; // avoid divide by zero

            var open = tickets.Count(t => t.Status == TicketStatus.Open);
            var resolved = tickets.Count(t => t.Status == TicketStatus.Resolved);
            var closedNoResolve = tickets.Count(t => t.Status == TicketStatus.Closed);

            var model = new
            {
                Total = total,
                OpenPercent = open * 100 / total,
                ResolvedPercent = resolved * 100 / total,
                ClosedNoResolvePercent = closedNoResolve * 100 / total
            };

            return View(model);
        }

    }
}
