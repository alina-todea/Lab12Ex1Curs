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
    public class TeachersService : ITeachersService
    {
        /// <summary>
        /// updates the teacher rank from intructor to Assistant Professor to associate professor to Professor
        /// </summary>
        /// <param name="teacherToCreate">teacher to update</param>
        public void UpdateTeacherRank(Teacher teacherToCreate)
        {
            //nu sunt sigura ca e ok ca ii dau direct Teachewr aici, am citit discustii contradictorii daca ar trebui s stam in layer, dr nu am stiut cum sa inserez teachwerToGet ca sa am Id
            using var ctx = new UniversityDbContext();

            switch (teacherToCreate.RankValue)
            {
                case "Instructor":
                    teacherToCreate.RankValue = "Assistant Professor";
                    break;
                case "Assistant Professor":
                    teacherToCreate.RankValue = "Associate Professor";
                    break;
                case "Associate Professor":
                    teacherToCreate.RankValue = "Professor";
                    break; //return NotModified();
                default:
                    break;

            }
            ctx.SaveChanges();
        }
        /// <summary>
        /// creates a new teacher
        /// </summary>
        /// <param name="teacherToCreate">teacher data</param>
        /// <param name="addressId">the id of the teacher's address</param>
        /// <returns>teacher data</returns>
        public async Task<TeacherToGet> CreateTeacherAsync(TeacherToCreate teacherToCreate, int addressId)
        {
            using var ctx = new UniversityDbContext();

            var teacher = new Teacher
            {
                Name = teacherToCreate.Name,
                RankValue = teacherToCreate.RankValue,
                AddressId = addressId
            };
            ctx.Teachers.Add(teacher);
            await ctx.SaveChangesAsync();

            return teacher.ToDto();
        }
        /// <summary>
        /// return the id of a teacher identfied by name and rank or 0 if not found
        /// </summary>
        /// <param name="teacherToCreate"></param>
        /// <returns></returns>
        public int GetTeacherIdByNameAndRank(TeacherToCreate teacherToCreate)
        {
            using var ctx = new UniversityDbContext();

            var existingTeacher = ctx.Teachers
                .FirstOrDefault(s => s.RankValue == teacherToCreate.RankValue
                && s.Name.ToLower().Replace(" ", "").Replace("-", "") == teacherToCreate.Name.ToLower().Replace(" ", "").Replace("-", ""));

            if (existingTeacher != null)
            {
                return existingTeacher.Id;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// returns a teacher by teacher id
        /// </summary>
        /// <param name="id">id of the teacher</param>
        /// <returns>teacher</returns>
        public Teacher GetTeacherById([Range(1, int.MaxValue)] int id)
        {
            using var ctx = new UniversityDbContext();

            Teacher myTeacher;

            myTeacher = ctx.Teachers.FirstOrDefault(s => s.Id == id);
            if (myTeacher == null)
            {
                return null;
            }

            return myTeacher;
        }
        /// <summary>
        /// links an existing address to a teacher
        /// </summary>
        /// <param name="teacherId">teacher id</param>
        /// <param name="addressId">address id</param>
        /// <returns></returns>
        public async Task UpdateAddressIdAsync(int teacherId, int addressId)
        {
            using var ctx = new UniversityDbContext();

            var existingTeacher = ctx.Teachers.FirstOrDefault(s => s.Id == teacherId);
            if (existingTeacher == null)
            {
                throw new InvalidIdException("teacher id  invalid");
            }
            var existingAddress = ctx.Addresses.FirstOrDefault(s => s.Id == addressId);
            if (existingAddress == null)
            {
                throw new InvalidIdException("address id  invalid");
            }
            else
            {
                existingTeacher.AddressId = addressId;
            }

            await ctx.SaveChangesAsync();

            return;
        }
        /// <summary>
        /// updates a teacher's data. updating the rank only for correcting purposes, otherwise use TeacherRankManagement
        /// </summary>
        /// <param name="id">teacher id</param>
        /// <param name="teacherData">teacher data</param>
        /// <returns>teacher</returns>
        /// <exception cref="InvalidIdException">invalid teaxher id</exception>
        public async Task<Teacher> UpdateTeacherDataAsync(int id, TeacherDataToUpdate teacherData)
        {
            using var ctx = new UniversityDbContext();

            var teacherToUpdate = ctx.Teachers.FirstOrDefault(s => s.Id == id);

            if (teacherToUpdate == null)
            {
                throw new InvalidIdException("teacher id invalid");
            }

            teacherToUpdate.Name = teacherData.Name;
            teacherToUpdate.RankValue = teacherData.RankValue;

            await ctx.SaveChangesAsync();

            return teacherToUpdate;
        }
        /// <summary>
        /// returns all teachers
        /// </summary>
        /// <returns>list of teachers</returns>
        public List<TeacherRankToGet> GetAllTeachers()
        {
            using var ctx = new UniversityDbContext();
            var allTeachers = ctx.Teachers.Select(x => new TeacherRankToGet(x)).ToList();
            return allTeachers;
        }
        /// <summary>
        /// deletes a teacher
        /// </summary>
        /// <param name="id">teacher id</param>
        /// <returns></returns>
        /// <exception cref="InvalidIdException">teacher not found</exception>
        public async Task DeleteTeacher([Range(1, int.MaxValue)] int id)
        {
            using var ctx = new UniversityDbContext();

            var teacher = await ctx.Teachers.FirstOrDefaultAsync(s => s.Id == id);

            if (teacher == null)
            {
                throw new InvalidIdException("teacher not found");
            }

            ctx.Teachers.Remove(teacher);
            await ctx.SaveChangesAsync();

            return;
        }
    }
}