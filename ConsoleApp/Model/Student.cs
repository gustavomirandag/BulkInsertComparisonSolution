using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Model
{
    public class Student
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Course Course { get; set; }
    }
}
