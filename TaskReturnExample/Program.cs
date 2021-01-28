using System;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;
using TaskReturnExample.Models;

namespace TaskReturnExample
{
    class Program
    {
        static async Task Main(string[] args)
        {

            Task<Egg> eggsTask = FryEggs(2);
            Task<Bacon> baconTask = FryBacon(3);
            Task<Toast> toastTask = MakeToastWithButterAndJamAsync(2);
            Coffee coffee = PourCoffee();
            Console.WriteLine("coffee is ready");
            Juice juice = PourOJAsync();
            Console.WriteLine("oj is ready");


            var BreakfastTasks = new List<Task> { eggsTask, baconTask, toastTask};
            while (BreakfastTasks.Count > 0)
            {
                Task finishedTask = await Task.WhenAny(BreakfastTasks);
                if (finishedTask == eggsTask)
                {
                    Console.WriteLine("eggs are ready");
                }
                else if (finishedTask == baconTask)
                {
                    Console.WriteLine("bacon is ready");
                }
                else if (finishedTask == toastTask)
                {
                    Console.WriteLine("toast is ready");
                }
                if (BreakfastTasks.TrueForAll(t => t.IsCompletedSuccessfully))
                {
                    Console.WriteLine("Breakfast is ready!");
                    break;
                }
            }
            Console.ReadLine();
        }

        private static Juice PourOJAsync()
        {
            Console.WriteLine("Pouring orange juice");
            return new Juice();
        }

        private static async Task<Toast> MakeToastWithButterAndJamAsync(int number)
        {
            var toast = await toastBreadAsync(number);
            ApplyButter(toast);
            ApplyJam(toast);

            return toast;
        }

        private static async Task<Toast> toastBreadAsync(int slices)
        {
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("Putting a slice of bread in the toaster");
            }
            Console.WriteLine("Start toasting...");
            await Task.Delay(3000);
            Console.WriteLine("Remove toast from toaster");

            return new Toast();
        }

        private static void ApplyJam(Toast toast) =>
            Console.WriteLine("Putting jam on the toast");

        private static void ApplyButter(Toast toast) =>
            Console.WriteLine("Putting butter on the toast");

        private static async Task<Bacon> FryBacon(int slices)
        {
            Console.WriteLine($"putting {slices} slices of bacon in the pan");
            Console.WriteLine("cooking first side of bacon...");
            Task.Delay(3000).Wait();
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("flipping a slice of bacon");
            }
            Console.WriteLine("cooking the second side of bacon...");
            await Task.Delay(3000);
            Console.WriteLine("Put bacon on plate");

            return new Bacon();
        }

        private static async Task<Egg> FryEggs(int howMany)
        {
            Console.WriteLine("Warming the egg pan...");
            await Task.Delay(3000);
            Console.WriteLine($"cracking {howMany} eggs");
            Console.WriteLine("cooking the eggs ...");
            await Task.Delay(3000);
            Console.WriteLine("Put eggs on plate");

            return new Egg();
        }

        private static  Coffee PourCoffee()
        {
            Console.WriteLine("Pouring coffee");
            return new Coffee();
        }
    }
}

