using System;
using System.Collections.Generic;

namespace Restaurant_Console.Classes
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public bool IsSend { get; set; }
        public Client Client { get; set; }
        public List<Dish> Dishes { get; set; }

        public Order()
        {
            OrderDate = DateTime.Now;
            IsSend = false;
            Dishes = new List<Dish>();
        }
    }
}
