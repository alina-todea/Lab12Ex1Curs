using DomainLayer.Models;
using DomainLayer.Data;
using Microsoft.EntityFrameworkCore;
using System.CodeDom;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

//Seed();

//DeleteStudent(2);
//ShowAlphabetically();

/*
static void DeleteStudent(int id, bool removeAddress)
{
    using var ctx = new UniversityDbContext();
    var student = ctx.Students.FirstOrDefault(x => x.Id == id);
    if (student == null)
    {
        return;
    }
    var address = ctx.Addresses.Where(x => x.StudentId == id).ToList();
    address.ToList().ForEach(x => x.StudentId = null);

    if (removeAddress)
    {
        address.ToList().ForEach(e => ctx.Remove(e));
    }

    ctx.Students.Remove(student);
    ctx.SaveChanges();

}
*/
/*
static void GroupByCriteria()
{
    using var ctx = new UniversityDbContext();

    var criteria = ctx.Students.GroupBy(x => new { Name = x.Last + x.First, Age = x.Age });

    foreach (var i in criteria)
    {
        var key = i.Key;
        Console.WriteLine($"{key.Name} {key.Age}");
        criteria.ToList().ForEach(e => Console.WriteLine(e));
    }
}
*/
/*static void Seed()
{
    using var ctx = new UniversityDbContext();


    ctx.Students.Add(new Student
    {
        Last = "Mickey",
        First = "Mouse",
        Age = 18,
    });
    ctx.Students.Add(new Student
    {
        Last = "Minnie",
        First = "Mouse",
        Age = 19,
    });
    ctx.Addresses.Add(new Address
    {
        Oras = "Oradea",
        Strada = "  Morii",
        Nr = 1,
        StudentId = 1
    }) ;
    ctx.Addresses.Add(new Address
    {
        Oras = "Oradea",
        Strada = "  Morii",
        Nr = 1,
        StudentId = 2
    });*/
/*
ctx.Students.Add(new Student
{
    Last = "Donald",
    First = "Duck",
    Age = 20,
});
ctx.Students.Add(new Student
{
    Last = "Daisy",
    First = "Duck",
    Age = 21,
});
ctx.Students.Add(new Student
{
    Last = "Della",
    First = "Duck",
    Age = 22,
});
ctx.Students.Add(new Student
{
    Last = "Scrooge",
    First = "McDuck",
    Age = 20,
});
ctx.Students.Add(new Student
{
    Last = "Louie",
    First = "Duck",
    Age = 23,
});
ctx.Students.Add(new Student
{
    Last = "Quackmore",
    First = "Duck",
    Age = 18,
});
ctx.Students.Add(new Student
{
    Last = "Huey",
    First = "Duck",
    Age = 22,
});
ctx.Students.Add(new Student
{
    Last = "Dewey",
    First = "Duck",
    Age = 19,
});
ctx.Students.Add(new Student
{
    Last = "Hortense",
    First = "Duck",
    Age = 23,
});

*
ctx.SaveChanges();


}*/
/*

static void ShowAlphabetically()
{
    using var ctx = new UniversityDbContext();
    foreach (var student in ctx.Students.OrderBy(e => e.Last).ThenBy(e => e.First))
    {
        Console.WriteLine(student);
    }

}
static void ShowFirstOverTwenty()
{
    using var ctx = new UniversityDbContext();

    ctx.Students
        .Where(e => e.Age > 20 && e.Degree == Degree.Constructii)
        .OrderBy(e => e.Age)
        .Take(1)
        .ToList()
        .ForEach(e => Console.WriteLine(e));
}
*/