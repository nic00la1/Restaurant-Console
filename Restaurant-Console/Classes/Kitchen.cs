using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Console.Classes
{
    public class Kitchen
    {
        private static readonly ConcurrentQueue<Order> _queue = new ConcurrentQueue<Order>();
        private static readonly SemaphoreSlim _signal = new SemaphoreSlim(0);
        private static bool _processorStarted;
        private static readonly object _startLock = new object();

        public static void ReceiveOrder(Order order)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));

            _queue.Enqueue(order);
            _signal.Release();

            StartProcessorIfNeeded();

            Console.WriteLine($"[Kuchnia] Zamówienie #{order.Id} otrzymane (elementów: {order.Dishes?.Count ?? 0}).");
        }

        private static void StartProcessorIfNeeded()
        {
            if (_processorStarted) return;
            lock (_startLock)
            {
                if (_processorStarted) return;
                _processorStarted = true;
                Task.Run(ProcessLoop);
            }
        }

        private static async Task ProcessLoop()
        {
            while (true)
            {
                await _signal.WaitAsync();
                if (_queue.TryDequeue(out var order))
                {
                    // Oznacz jako "wysłano / w kuchni"
                    order.IsSend = true;
                    Console.WriteLine($"[Kuchnia] Przetwarzanie zamówienia #{order.Id} ...");

                    // Symuluj przygotowanie (milisekundy na danie)
                    int delayMs = Math.Max(400, (order.Dishes?.Count ?? 1) * 600);
                    try
                    {
                        await Task.Delay(delayMs);
                        Console.WriteLine($"[Kuchnia] Zamówienie #{order.Id} jest GOTOWE.");
                    }
                    catch (TaskCanceledException) { /*ignoruj*/ }
                }
            }
        }

        public static void PrintQueueSnapshot()
        {
            Console.WriteLine($">>> Migawka kolejki kuchennej: {_queue.Count} elementów ");
            foreach (var o in _queue.ToArray())
            {
                Console.WriteLine($"- #{o.Id} elementów: {o.Dishes?.Count ?? 0} (Czy wysłane: {o.IsSend})");
            }
        }
    }
}
