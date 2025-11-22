using Restaurant_Console.Classes;
using System;

namespace Restaurant_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Restauracja");

            foreach (var d in Dish.Dishes)
            {
                Console.WriteLine("{0,2}. {1,-30} {2,8:C}", d.Id, d.Name, d.Price);
            }
        }
    }
}