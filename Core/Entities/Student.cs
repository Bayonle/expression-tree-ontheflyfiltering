using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Bogus;
using mambuquery.api.Core.Models;

namespace mambuquery.api.Core.Entities
{
    public class Student
    {
        [Description("Id")]
        public long Id { get; set; }
        [Description("First Name")]
        public string FirstName { get; set; }
        [Description("Last Name")]
        public string LastName { get; set; }
        [Description("Age")]
        public int? Age { get; set; }
        [Description("Balance")]
        public decimal? Balance { get; set; }
        [Description("Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        public static IEnumerable<Student> StudentList = new List<Student>();

        public static IEnumerable<Student> GetStudents()
        {
            if (StudentList.Count() == 0)
            {
                var id = 1;
                var testStudents = new Faker<Student>()
                .RuleFor(s => s.Id, f => id++)
                .RuleFor(s => s.FirstName, (f) => f.Person.FirstName)
                .RuleFor(s => s.LastName, (f) => f.Person.LastName)
                .RuleFor(s => s.Age, (f) => f.Random.Number(18, 50))
                .RuleFor(s => s.Balance, (f) => f.Random.Decimal(1000M, 2000000M))
                .RuleFor(s => s.DateOfBirth, (f) => f.Person.DateOfBirth).Generate(20);

                testStudents.Add(new Student{
                    FirstName = null,
                    LastName = "Adewale",
                    DateOfBirth = DateTime.Now,
                    Age = 30,
                    Balance = 2000M
                });
                testStudents.Add(new Student{
                    FirstName = "",
                    LastName = "Yaya",
                    DateOfBirth = DateTime.Now,
                    Age = 25,
                    Balance = 3000M
                });
                testStudents.Add(new Student{
                    FirstName = "Johnson",
                    LastName = "",
                    DateOfBirth = DateTime.Now,
                    Age = 25,
                    Balance = null
                });
                testStudents.Add(new Student{
                    FirstName = "Frank",
                    LastName = "Lampard",
                    DateOfBirth = DateTime.Now,
                    Age = null,
                    Balance = 20000M
                });

                StudentList = testStudents;
            }

            return StudentList;

        }



    }


}