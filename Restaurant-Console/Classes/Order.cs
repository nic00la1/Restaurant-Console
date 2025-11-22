using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Console.Classes
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public bool IsSend { get; set; }
        public Client Client { get; set; }
        public List<Dish> Dishes { get; set; }
    }
}
