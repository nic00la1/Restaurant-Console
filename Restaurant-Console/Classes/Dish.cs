using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Console.Classes
{
    public class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        // Lista dań
        public static List<Dish> Dishes = new List<Dish>() {
            new Dish(){ Id = 1, Name = "Rosół", Price = 15 },
            new Dish(){ Id = 2, Name = "Kapuśniak", Price = 12 },
            new Dish(){ Id = 3, Name = "Pomidorowa", Price = 10 },
            new Dish(){ Id = 4, Name = "Kurczak z ziemniakami", Price = 20 },
            new Dish(){ Id = 5, Name = "Łosoś z pieca", Price = 60 },
            new Dish(){ Id = 6, Name = "Spaghetti", Price = 60 }
        };
    }
}
