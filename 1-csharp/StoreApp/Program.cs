using System;
using System.Collections.Generic;

namespace StoreApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var products = new List<Product>();
            var coffee = new Product();
            coffee.Name = "coffee";
            coffee.Price = 5.0;
            products.Add(coffee);
            var table = new Product();
            table.Name = "table";
            table.Price = 50.0;
            products.Add(table);

            Console.WriteLine("Enter a new price for the table: ");
            string input = Console.ReadLine();
            double newPrice = double.Parse(input);
            table.Price = newPrice;

            DisplayProductList(products);
        }

        static void DisplayProductList(List<Product> products)
        {
            foreach (var product in products)
            {
                Console.WriteLine($"{product.Name}\t${product.Price}");
            }
        }
    }
}
