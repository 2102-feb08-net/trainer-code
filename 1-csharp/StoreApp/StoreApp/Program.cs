using System;
using System.Collections.Generic;

namespace StoreApp
{
    class Program
    {
        // don't repeat yourself (DRY principle)
        // separation of concerns
        //     different classes, methods, projects, anything
        //     should have their own distinct concerns, responsibilities
        //  e.g. separate the classes for user input & output
        //     from all the rest of the classes


        // a project is a collection of classes (and other types)
        // that will all be compiled together (into an assembly)

        // roughly two kinds of projects -
        //   ones that can run on their own (application projects, like console app)
        //        (has a Main method)
        //   ones that can't run on their own (library projects)
        //        (doesn't have a Main method)
        //


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
