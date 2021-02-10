using System;
using Humanizer;

namespace HelloVisualStudio
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Inventory App 1.0");
            Console.Write("What item do you have? ");
            string item = Console.ReadLine(); // computer, apple
            Console.Write("How many do you have? ");
            int quantity = int.Parse(Console.ReadLine()); // 2, 6

            if (quantity != 1)
            {
                item = Pluralize(item);
            }

            Console.WriteLine($"You have {quantity} {item}.");
        }

        static string Pluralize(string item)
        {
            return item.Pluralize();
        }
    }
}
