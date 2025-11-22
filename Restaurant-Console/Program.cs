using Restaurant_Console.Classes;
using Restaurant_Console.Menu;
using System;

namespace Restaurant_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu.Menu.StartMenu();
            Dish.DisplayDishes();
        }
    }
}