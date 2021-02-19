using System;
using System.Collections.Generic;
using System.Text;

namespace EfDbFirstDemo.ConsoleApp
{
    public class Student
    {
        public string Id { get; }
        public string Name { get; }

        public Student(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
