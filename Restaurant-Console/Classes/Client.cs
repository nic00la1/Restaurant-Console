using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Console.Classes
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<Order> HistoryOfOrders { get; set; }

        public Client()
        {
            HistoryOfOrders = new List<Order>();
        }

        // Złóż zamówienie
        public void PlaceOrder(Order order)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));
            order.Client = this;
            HistoryOfOrders.Add(order);
        }

        public override string ToString()
        {
            return $"{Name} {Surname} (Id: {Id})";
        }
    }
}
