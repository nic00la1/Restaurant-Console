using Restaurant_Console.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Console.Menu
{
    public class Menu
    {
        static string[] pozycjeMenu = { "Wyświetl Menu dań", "Złóż zamówienie", "Pokaż przyjęte zamówienia", "Koniec"};
        static int aktywnaPozycjaMenu = 0;
        static Waiter waiter = new Waiter("Jan");

        public static void StartMenu()
        {
            Console.Title = "Restauracja";
            Console.CursorVisible = false;

            while (true)
            {
                PokazMenu();
                WybieranieOpcji();
                UruchomOpcje();
            }
        }

        static void PokazMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(">>> Restauracja <<<\n");

            for (int i = 0; i < pozycjeMenu.Length; i++) 
            {
                if (i == aktywnaPozycjaMenu)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("{0, -35}", pozycjeMenu[i]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                }    
                else Console.WriteLine(pozycjeMenu[i]);
            }
        }

        static void WybieranieOpcji()
        {
            do
            {
                ConsoleKeyInfo klawisz = Console.ReadKey();
                if (klawisz.Key == ConsoleKey.UpArrow) // strzałka w górę
                {
                    aktywnaPozycjaMenu = (aktywnaPozycjaMenu > 0) ? aktywnaPozycjaMenu - 1 : pozycjeMenu.Length - 1;
                    PokazMenu();
                }
                else if (klawisz.Key == ConsoleKey.DownArrow) // strzałka w dół
                {
                    aktywnaPozycjaMenu = (aktywnaPozycjaMenu + 1) % pozycjeMenu.Length;
                    PokazMenu();
                }
                else if (klawisz.Key == ConsoleKey.Escape)
                {
                    aktywnaPozycjaMenu = pozycjeMenu.Length - 1; // Koniec
                    break;
                }
                else if (klawisz.Key == ConsoleKey.Enter)
                    break;
            } while (true);
        }

        static void UruchomOpcje()
        {
            switch (aktywnaPozycjaMenu)
            {
                case 0: Console.Clear(); DisplayMenuDishes(); break; 
                case 1: Console.Clear(); waiter.TakeOrderInteractive(); break; 
                case 2: Console.Clear(); waiter.PrintAcceptedOrders(); break; 
                case 3: Environment.Exit(0); break; 
            }
        }

        static void DisplayMenuDishes()
        {
            Dish.DisplayDishes();
        }
    }
}
