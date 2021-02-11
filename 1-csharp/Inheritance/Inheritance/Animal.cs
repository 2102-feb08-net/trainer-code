using System;
using System.Collections.Generic;
using System.Text;

namespace Inheritance
{
    // inheritance allows a new class to automatically have all the behavior and data structure of an existing class.
    //  the language will enable substituting an instance of the new class wherever an instance of the existing would be accepted.
    //     (Liskov substitution principle) (allow for polymorphism)

    // this is a way to achieve code reuse
    // often, a better way is delegation/composition
    //    (problem: class A wants to reuse some behavior from class B)
    //    (inheritance: find some way to make A fit in a subclass of B)
    //    (composition: give class A a field to contain an instance of class B, and call whatever methods on it you need)

    // an abstract class is a class that by itself cannot be used as a template for objects
    //   it's effectively an incomplete class definition that needs to be completed by some derived class.

    public abstract class Animal : IOrganism
    {
        public int NumberOfLegs { get; }
        public bool CanFly { get; }
        public string Kind { get; }
        public string Name { get; set; }

        // null is a valid value for reference type variables, not value type.
        // HOWEVER, there is a special class Nullable<T> which can wrap value types and allow them to have null.
        // int? x = null;
        // this language feature is called nullable value types https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/nullable-value-types

        public Animal(int numberOfLegs, bool canFly, string kind, string name)
        {
            NumberOfLegs = numberOfLegs;
            CanFly = canFly;
            // validation in the constuctor
            Kind = kind ?? throw new ArgumentNullException(nameof(kind));
            Name = name ?? throw new ArgumentNullException(nameof(name));

            // ?? is an operator that checks the left hand side, if it's not null, the result is that value.
            //     but if it is null, the right hand side is evaluated instead, as a fallback value
            PrintMessage();
        }

        // abstract classes can have abstract methods and properties
        // those are the ones which subclasses must provide some implementation for.
        //    the parent abstract class defines the name, parameters, and return type of the method.
        //    each subclass can implement it differently.
        public abstract void Move();

        public virtual void PrintMessage()
        {
            Console.WriteLine("message from Animal");
        }

        // an interface is like taking the idea of an abstract class further.
        // interfaces don't contain implementation of methods or properties, and don't have fields.
            // all they have is method declarations, property declarations
            // effectively every member of an interface is abstract

        // it's very OK for a class to implement (derive from) multiple interfaces
    }
}
