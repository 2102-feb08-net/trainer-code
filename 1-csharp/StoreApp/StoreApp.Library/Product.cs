using System;

// usually, namespaces follow the folder heirarchy
// so, e.g. MyApp.Objects.Utility.Collection
//   might be in the MyApp.Objects/Utility subfolder

namespace StoreApp.Library
{
    // encapsulation: a class should own its own data and enforce rules on it
    //    one important kind of rule goes like "a valid ____ always has _____"

    // in C#, the only way to make an instance of a class (object)
    // is by calling one of the class's constructors.

    // when you design a class, you get to decide what are all the valid ways to
    // make a new instance of that class. each one will be a constructor.
    public class Product
    {
        // the method name & return type are combined in a ctor

        // this ctor here (zero arguments, no contents)
        //  is added by default to every class without an explicit written ctor.
        // public Product()
        // {
        // }

        public Product(string name)
        {
            // "this" is a keyword that refers to the current instance of this class
            Name = name;
        }

        // public class members: can be referenced outside their class.
        // private class members: cannot. (private is the default)

        // fields
        private string _name;
        private double _price;

        public string Name
        {
            get { return _name; }
            set
            {
                // "value" is whatever the other code put after the equals sign
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("invalid name");
                }
                _name = value;
            }
        }
        // these properties are what we'd call "full properties"

        public double Price
        {
            get { return _price; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("invalid price");
                }
                _price = value;
            }
        }

        // there's also "auto-properties":
        public string Id { get; set; }
        // (there's a hidden backing field added by the compiler, but i don't have to write it)


        // often in code we want a custom data type that can hold only one of a set collection of possible values.
        // like how bool only allows true and false, unlike string which has infinite possible values

        // so, you can define your own enumerations, or "enum" types.
        private DayOfWeek _saleDay = DayOfWeek.Friday;

        // public bool CheckValidity()
        // {
        //     if (Price > 0) return true;
        // }

        // java-style: getter and setter methods to enforce encapsulation around data.
        // but c# style- just use properties.
        //     properties have aspects of both fields and methods.
        // public string GetName()
        // {
        //     return _name;
        // }

        // public void SetName(string name)
        // {
        //     if (string.IsNullOrWhiteSpace(name))
        //     {
        //         throw new ArgumentException("invalid name");
        //     }
        //     _name = name;
        // }


        // public double GetPrice()
        // {
        //     return _price;
        // }

        // public void SetPrice(double price)
        // {
        //     if (price < 0)
        //     {
        //         throw new ArgumentException("invalid price");
        //     }
        //     _price = price;
        // }
    }
}
