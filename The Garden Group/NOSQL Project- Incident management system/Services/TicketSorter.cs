using NOSQL_Project__Incident_management_system.Models;

namespace NOSQL_Project__Incident_management_system.Services
{
    public class TicketSorter
    {
        private int GetRank(string priority)
        {
            return priority switch
            {
                "Urgent" => 1,
                "High" => 2,
                "Medium" => 3,
                "Low" => 4,
                _ => 5
            };
        }

        public List<Ticket> SortByPriority(List<Ticket> tickets)
        {
            return tickets.OrderBy(t => GetRank(t.Priority)).ToList();
        }
    }
}
