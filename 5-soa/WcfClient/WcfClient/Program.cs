using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfClient.WcfServiceReference;

namespace WcfClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("welcome to wcf client");
            Console.WriteLine("enter a number:");

            int input = int.Parse(Console.ReadLine());

            using (var client = new Service1Client())
            {

                string result = client.GetData(input);
                Console.WriteLine($"result: {result}");
            }

            Console.ReadKey();
        }
    }
}
