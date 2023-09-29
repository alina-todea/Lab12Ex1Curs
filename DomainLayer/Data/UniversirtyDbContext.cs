using System;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;


namespace DomainLayer.Data
{
    public class UniversityDbContext : DbContext

    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Mark> Marks { get; set; }
        //public DbSet<Rank> Ranks { get; set; }


        public DbSet<StudentEnrollment> StudentEnrollments { get; set; }
        public DbSet<TeacherAssignement> TeacherAssignements { get; set; }


        public UniversityDbContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql(@"Host=localhost;Username=postgres;Password=postgres;Database='cursdotnetlab12ex1'");
    }
}
