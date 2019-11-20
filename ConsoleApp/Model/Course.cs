using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Model
{
    public class Course
    {
        public Guid Id { get; set; }
        public ICollection<Subject> Subjects { get; set; }

        public Course()
        {
            Subjects = new List<Subject>();
        }
    }
}
