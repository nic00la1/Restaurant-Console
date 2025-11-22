using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Console.Classes
{
    public class Waiter
    {
        // Napisz program, który pozwoli kelnerowi:
        // 1. przyjmować zamówienia od klientów 
        // 2. przekazywać zamówienia do kuchni
        // 3. generować rachunki na podstawie zamówień
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<Order> AcceptedOrders { get; set; }
    }
}
