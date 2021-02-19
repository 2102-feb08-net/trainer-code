using System;
using System.Collections.Generic;

namespace EfDbFirstDemo.ConsoleApp
{
    public class Course
    {

        public string CourseId { get; }

        public HashSet<Student> Students { get; set; } = new HashSet<Student>();

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("invalid value");
                _name = value;
            }
        }

        public Course(string courseId, string name)
        {
            CourseId = courseId;
            Name = name;
        }
    }
}