using System;
using System.ComponentModel.DataAnnotations;
using CatalogAPI.DTOs;
using CatalogAPI.Extensions;
using CatalogAPI.Filters;
using DomainLayer.Data;
using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Services
{
    public class StudentsService : IStudentsService
    {
        /// <summary>
        /// creates a new student
        /// </summary>
        /// <param name="studentToCreate">student data</param>
        /// <param name="addressId">the id of the student's address</param>
        /// <returns>teacher data</returns>
        public async Task<StudentToGet> CreateStudentAsync(StudentToCreate studentToCreate, int addressId)
        {
            using var ctx = new UniversityDbContext();

            var student = new Student
            {
                Name = studentToCreate.Name,
                Age = studentToCreate.Age,
                AddressId = addressId
            };
            ctx.Students.Add(student);
            await ctx.SaveChangesAsync();

            return student.ToDto();
        }


        /// <summary>
        /// returns the student id if exists(by name and age) or 0 if student does not exist
        /// </summary>
        /// <param name="studentToCreate">student data</param>
        /// <returns>student id or 0</returns>
        public int GetStudentIdByNameAndAge(StudentToCreate studentToCreate)
        {
            using var ctx = new UniversityDbContext();

            var existingStudent = ctx.Students.FirstOrDefault(s => s.Name == studentToCreate.Name && s.Age == studentToCreate.Age);

            if (existingStudent != null)
            {
                return existingStudent.Id;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// returns a student data and the address
        /// </summary>
        /// <param name="id">student id</param>
        /// <param name="includeAddress">if true the address is also returned</param>
        /// <returns>student and address</returns>
        public Student GetStudentByIdWithAddress([Range(1, int.MaxValue)] int id, bool includeAddress)
        {

            using var ctx = new UniversityDbContext();

            Student myStudent;

            if (includeAddress)
            {
                myStudent = ctx.Students.Include(s => s.Address).FirstOrDefault(s => s.Id == id);

                if (myStudent == null)
                {
                    throw new InvalidIdException("student id or address invalid");
                }

            }
            else
            {
                myStudent = ctx.Students.FirstOrDefault(s => s.Id == id);
                if (myStudent == null)
                {
                    throw new InvalidIdException("student id invalid");
                }
            }

            return myStudent;
        }
        /// <summary>
        /// returns all students
        /// </summary>
        /// <returns>list of students</returns>
        public List<StudentDataToGet> GetAllStudents()
        {
            using var ctx = new UniversityDbContext();
            var allStudents = ctx.Students.Select(x => new StudentDataToGet(x)).ToList();
            return allStudents;
        }

        /// <summary>
        /// updates the address id of a student
        /// </summary>
        /// <param name="studentId">student id</param>
        /// <param name="addressId">address id</param>
        /// <returns></returns>
        public async Task UpdateAddressIdAsync(int studentId, int addressId)
        {
            using var ctx = new UniversityDbContext();

            var existingStudent = ctx.Students.FirstOrDefault(s => s.Id == studentId);
            if (existingStudent == null)
            {
                return;
            }
            var existingAddress = ctx.Addresses.FirstOrDefault(s => s.Id == addressId);
            if (existingAddress == null)
            {
                return;
            }
            else
            {
                existingStudent.AddressId = addressId;
            }

            await ctx.SaveChangesAsync();

            return;
        }
        /// <summary>
        /// updates srtudent name and age
        /// </summary>
        /// <param name="id">student id</param>
        /// <param name="studentData">new student data</param>
        /// <returns></returns>
        /// <exception cref="InvalidIdException">student id invalid</exception>
        public async Task<Student> UpdateStudentDataAsync(int id, StudentDataToUpdate studentData)
        {
            using var ctx = new UniversityDbContext();

            var studentToUpdate = ctx.Students.FirstOrDefault(s => s.Id == id);

            if (studentToUpdate == null)
            {
                throw new InvalidIdException("student id invalid");
            }

            studentToUpdate.Name = studentData.Name;
            studentToUpdate.Age = studentData.Age;

            await ctx.SaveChangesAsync();

            return studentToUpdate;
        }
        /// <summary>
        /// deletes a student
        /// </summary>
        /// <param name="id">student id</param>
        /// <returns></returns>
        /// <exception cref="InvalidIdException">student not found</exception>
        public async Task DeleteStudentAsync([Range(1, int.MaxValue)] int id)
        {
            using var ctx = new UniversityDbContext();

            var stud = await ctx.Students.FirstOrDefaultAsync(s => s.Id == id);

            if (stud == null)
            {
                throw new InvalidIdException("student not found");
            }

            ctx.Students.Remove(stud);
            await ctx.SaveChangesAsync();

            return;
        }
    }
}