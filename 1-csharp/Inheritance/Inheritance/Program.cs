using System;

namespace Inheritance
{
    // a static class can contain only static members
    // and can never be instantiated or inherited from
    // just a container for some related data & behavior
    //   not very object oriented
    static class Program
    {
        const int x = 4;

        static void Main(string[] args)
        {
            //var dog = new Animal(4, false, "dog", "Fido");
            var fred = new Human();

            InspectOrganism(fred);
        }

        static void InspectOrganism(IOrganism x)
        {
        }
    }
}
