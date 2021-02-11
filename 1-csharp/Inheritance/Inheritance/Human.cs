using System;
using System.Collections.Generic;
using System.Text;

namespace Inheritance
{
    // human is a derived class of animal

    // "child class" "subclass" / "parent class" "superclass" "base class"

    // access modifiers
    // - public
    // - internal
    // - protected (visible to the current class and any subclasses)
    // - private (visible to the current class but not to subclasses)
    // - protected internal
    // - private protected
    // https://stackoverflow.com/a/49597029/624710

    // other modifiers:

    // - const
    // - abstract
    // - new
    // - virtual
    // - override
    // - sealed
    // - static

    // - async

    // - ref
    // - out
    // - in
    public class Human : Animal
    {
        // readonly field vs get-only auto-property
        //private readonly object _dependency;
        //private object _dependency { get; }

        public Human(string name) : base(2, false, "human", name)
        {
            // every child class constructor has to call one parent class constructor first.
            // the implicit default is a zero-argument constructor.
            // the explicit syntax for this is ": base(...)"
        }

        public Human() : this("John Doe")
        {
            // this ctor runs the other ctor first
        }

        // from a certain point of view, methods and fields and properties and everything else ARE inherited
        //    but constructors AREN'T.

        // in C#, inheritance is allowed by default, but overriding is prohibited by default.
        //        (you can only override virtual things, including abstract things)
        //   (encouraging extending classes with new behavior, discouraging changing existing behavior from the parent class)
        // method overriding replaces the implementation of the parent class with something new.
        public override void Move()
        {
        }

        // 
        public override void PrintMessage()
        {
            Console.WriteLine("message from Human");
            base.PrintMessage(); // overriding "replaces" that implementation, but it's still reusable under the base keyword if you want it
        }

        // C# allows something very unusual and strange and bad called method hiding.
        // child classes can define methods with the same name as methods from the parent class without overriding them.
        //     in a situation of method hiding, both implementations are in there, and which method is called depends on 
        //     the type of the variable you call the method with.
        // use the "new" modifier if you really want this for some reason
    }
}
