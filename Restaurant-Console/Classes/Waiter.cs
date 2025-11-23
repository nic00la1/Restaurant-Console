using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

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

        public Waiter()
        {
            AcceptedOrders = new List<Order>();
            Name = "Kelner";
        }

        public Waiter(string name, string surname = "")
        {
            Name = name ?? "Kelner";
            Surname = surname;
            AcceptedOrders = new List<Order>();
        }

        // Przyjmuje zamówienie od klienta i rejestruje je lokalnie.
        public void AcceptAnOrderFromAClient(Client client, Order order)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (order == null) throw new ArgumentNullException(nameof(order));

            // Powiąż zamówienie z klientem i zarejestruj historii klienta
            order.Client = client;
            client.PlaceOrder(order);

            // Oznacz jako przyjęte
            order.IsSend = false;

            AcceptedOrders.Add(order);

            Console.WriteLine($"[{Name}] Przyjęto zamówienie #{order.Id} od klienta {client} (pozycji: {order.Dishes?.Count ?? 0}).");
        }

        // Interaktywna metoda przyjmowania zamówienia - Menu wywoła tę metodę, żeby nie zaśmiecać Menu.cs
        public void TakeOrderInteractive()
        {
            Console.CursorVisible = true;
            Console.WriteLine("Nowe zamówienie - podaj dane klienta.");

            Console.Write("Id Klienta (numer): ");
            int clientId = 0;
            int.TryParse(Console.ReadLine(), out clientId);

            Console.Write("Imię: ");
            string name = Console.ReadLine();

            Console.Write("Nazwisko: ");
            string surname = Console.ReadLine();

            var client = new Client { Id = clientId, Name = name, Surname = surname};

            var order = new Order();
            order.Id = AcceptedOrders.Any() ? AcceptedOrders.Max(o => o.Id) + 1 : 1;
            order.OrderDate = DateTime.Now;
            order.Client = client;
            order.Dishes = new List<Dish>();
            order.IsSend = false;

            Console.WriteLine("\nDostępne dania:");
            foreach (var d in Dish.Dishes)
                Console.WriteLine("{0,2}. {1,-30} {2,8:C}", d.Id, d.Name, d.Price);

            while (true)
            {
                Console.Write("\nID Dania (puste, aby skończyć): ");
                var line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) break;
                if (!int.TryParse(line, out int dishId))
                {
                    Console.WriteLine("Nieprawidłowe id.");
                    continue;
                }

                var dish = Dish.Dishes.FirstOrDefault(d => d.Id == dishId);
                if (dish == null)
                {
                    Console.WriteLine("Danie nie znalezione.");
                    continue;
                }

                Console.Write("Ilość: ");
                if (!int.TryParse(Console.ReadLine(), out int ilosc) || ilosc <= 0)
                {
                    Console.WriteLine("Nieprawidłowa ilość.");
                    continue;
                }

                for (int i = 0; i < ilosc; i++)
                    order.Dishes.Add(dish);

                Console.WriteLine($"Dodano: {dish.Name} x{ilosc}");
            }
                if (!order.Dishes.Any()) {
                    Console.WriteLine("Brak pozycji w zamówieniu. Anulowano. Naciśnij dowolny klawisz...");
                    Console.CursorVisible = false;
                    Console.ReadKey();
                    return;
                }

                // Zarejetruj zamówienie
                AcceptAnOrderFromAClient(client, order);

                // Wydruk prostego rachunku
                Console.WriteLine("\nRachunek: ");
                foreach (var g in order.Dishes.GroupBy(d => d.Id))
                {
                    var d = g.First();
                    var quantity = g.Count();
                    decimal lineD = quantity * d.Price;
                    Console.WriteLine($"{d.Name, -30} x{quantity,2} {lineD,8:C}");
                }

                var total = order.Dishes.GroupBy(d => d.Id).Sum(g => g.Count() * g.First().Price);
                Console.WriteLine(new string('-', 40));
                Console.WriteLine($"SUMA: {total, 30:C}");

                Console.CursorVisible= false;
                Console.WriteLine("\nNaciśnij dowolny klawisz, aby wrócić do menu...");
                Console.ReadKey();
            }

        // Znajdź zamówienie po Id
        public Order GetOrderById(int id) => AcceptedOrders.FirstOrDefault(o => o.Id == id);

        // Wypisz listę przyjętych zamówień
        public void PrintAcceptedOrders()
        {
            Console.WriteLine($">>> Zarejestrowano zamówienia ({AcceptedOrders.Count}): <<<");
            foreach (var o in AcceptedOrders.OrderBy(x => x.Id))
            {
                var total = (o.Dishes ?? new List<Dish>())
                    .GroupBy(d => d.Id)
                    .Sum(g => g.Count() * g.First().Price);
                Console.WriteLine($"- #{o.Id} klient: {(o.Client != null ? o.Client.ToString() : "brak")}" +
                    $"  items: {o.Dishes?.Count ?? 0}  total: {total:C}");
            }

            Console.WriteLine("\nPress any key...");
            Console.ReadKey();
        }
    }
}
