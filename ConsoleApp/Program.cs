using ConsoleApp.Contexts;
using ConsoleApp.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using EFCore.BulkExtensions;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Unique Context
            var context = new SchoolContext();

            //Stopwatchs
            var oneByOneInsertStopWatch = new Stopwatch();
            var bulkInsertStopWatch = new Stopwatch();

            //OneByOne Insert
            var students = GenerateStudents(1000);
            oneByOneInsertStopWatch.Reset();
            oneByOneInsertStopWatch.Start();
            OneByOneInsert(context, students);
            oneByOneInsertStopWatch.Stop();
            Console.WriteLine($"OneByOne Insert - {oneByOneInsertStopWatch.ElapsedMilliseconds}");

            //Bulk Insert
            var bulkStudents = GenerateStudents(1000);
            bulkInsertStopWatch.Reset();
            bulkInsertStopWatch.Start();
            BulkInsert(context, bulkStudents);
            bulkInsertStopWatch.Stop();
            Console.WriteLine($"Bulk Insert - {bulkInsertStopWatch.ElapsedMilliseconds}");


        }

        private static IList<Student> GenerateStudents(int studentsQty)
        {
            var students = new List<Student>();
            for (int i = 0; i < studentsQty; i++)
            {
                var student = new Student
                {
                    Id = Guid.NewGuid(),
                    Name = Guid.NewGuid().ToString(),
                    Course = new Course
                    {
                        Id = Guid.NewGuid(),
                        Subjects = new List<Subject>
                        {
                            new Subject { Id = Guid.NewGuid() },
                            new Subject { Id = Guid.NewGuid() },
                            new Subject { Id = Guid.NewGuid() }
                        }
                    }
                };
                students.Add(student); ;
            }
                
            return students;
        }

        private static void OneByOneInsert(SchoolContext context, IEnumerable<Student> students)
        {
            foreach(var student in students)
                context.Add(student);
            context.SaveChanges();
        }

        private static void BulkInsert(SchoolContext context, IList<Student> students)
        {
            context.BulkInsert(students, new BulkConfig {BulkCopyTimeout = 999999 });
            context.SaveChanges();
        }
    }
}
