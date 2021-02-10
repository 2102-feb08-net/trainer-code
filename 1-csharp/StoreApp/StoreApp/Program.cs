using System;
using System.Collections;
using System.Collections.Generic;
using StoreApp.Library;

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

        // dotnet add reference ../StoreApp.Library/

        static void Main(string[] args)
        {
            var products = new List<Product>();
            var coffee = new Product("coffee");
            coffee.Price = 5.0;
            products.Add(coffee);
            var table = new Product("table");
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

        // arrays and other collections in C#
        static void Collections()
        {
            // array is the most "low-level" collection in C#
            int[] numArray = new int[6];
            // arrays have a defined data type they can contain
            //  and a fixed length.
            Console.WriteLine("Enter a length: ");
            int[] secondArray = new int[int.Parse(Console.ReadLine())];
            secondArray[0] = 3;
            secondArray[1] = 5; // now the array has: {3, 5, 0, 0, 0, ...

            // arrays have a convenient "initializer" syntax
            int[] thirdArray = { 4, 3, 7, 6 + 5, int.Parse(Console.ReadLine()) };

            // we can represent something 2D by nesting arrays inside arrays
            int[][] numberGrid = new int[3][];
            numberGrid[0] = new[] { 3, 4 };
            numberGrid[1] = new[] { 3, 7 };
            Console.WriteLine(numberGrid[0][1]); // 4

            // ^ that is called "jagged arrays"

            // C# also has "multi-dimensional arrays"
            int[,] twoDGrid = new int[3, 2];
            int[,] twoDGrid2 = {
                { 2, 3, 4 },
                { 2, 3, 4 },
                { 2, 3, 4 }
            };
            Console.WriteLine(twoDGrid2[2, 3]);

            // because of the limitations of arrays being fixed length,
            // they added a class named ArrayList that could grow and shrink.

            var list = new ArrayList();
            list.Add(1);
            list.Add(2);
            list.Add(true);
            list.Remove(1);
            int value = (int)list[0];

            // you can cast values with () to tell the compiler
            // "you might not know for sure it's this data type, but i wrote the logic and i am sure"

            // downside of ArrayList - it can contain any data type all at once,
            // and the compiler can't guarantee what data type will come out

            // then C# got generic types.
            // List<T> class has best of both worlds -
            //  1. it can grow and shrink
            //  2. it can enforce and guarantee the data type that can enter and exit

            // a generic class like List<T>, when you construct it, you must provide, not value parameters,
            // but a type parameter.

            // var list2 = new List<string>();
            // list2.Add("asdf");
            var list2 = new List<string> { "asdf" };
            // collection initialization syntax is not just for arrays
            // it works with any IEnumerable with an Add method (including your own)

            string value2 = list2[0];

            // we have other generic collections besides List for different needs

            var set = new HashSet<string>();
            // mathematical idea of a set - has no particular order, and has no duplicates
            set.Add("a"); // size 1
            set.Add("a"); // size 1

            // List vs HashSet:
            // checking if it contains an item: List has to search linearly, HashSet can check immediately
            if (set.Contains("a")) { }
            if (list.Contains("a")) { }

            var map = new Dictionary<string, string>();
            map["apple"] = "red fruit";

            var productQuantities = new Dictionary<string, List<int>>
            {
                ["chair"] = new List<int> { 14 },
                ["door"] = new List<int> { 1 },
            };
            // like HashSet, it's very fast to access a particular value

            // https://docs.microsoft.com/en-us/dotnet/standard/collections/

            // contrary to Java
            //  since we have a unified type system, we don't have the boxing problem
            //  with int vs Integer and so forth
            // even though strings are reference types, == for strings still compares by value.

        }
    }
}
