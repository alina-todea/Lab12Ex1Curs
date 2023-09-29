using System;
using DomainLayer.Data;
using DomainLayer.Models;

using System.ComponentModel.DataAnnotations;
using CatalogAPI.Extensions;
using CatalogAPI.DTOs;
using CatalogAPI.Filters;

namespace CatalogAPI.Services
{
    public class TeacherAssignementsServices : ITeacherAssignementsServices
    {
        /// <summary>
        /// removes a teacher's assignement to a subject
        /// </summary>
        /// <param name="id">id of the item (teacher, subject)</param>
        /// <param name="type">teacher/subject</param>
        /// <returns>id of the item (teacher, subject)</returns>
        public async Task<int> RemoveTeacherFromSubjectByIdAsync(int id, [RegularExpression("teacher|subject", ErrorMessage = "Invalid Rank")] string type)

        {
            using var ctx = new UniversityDbContext();
            if (type == "teacher")
            {
                var teacherToRemove = ctx.TeacherAssignements.Where(s => s.TeacherId == id).ToList();

                if (teacherToRemove != null)
                {
                    //teacherassignement.ToList().ForEach(s => s.IsActiveEnrollment = false);
                    teacherToRemove.ToList().ForEach(s => s.TeacherId = null);
                }
            }
            else if (type == "subject")
            {
                var teacherToRemove = ctx.TeacherAssignements.Where(s => s.SubjectId == id).ToList();

                if (teacherToRemove != null)
                {
                    //teacherassignement.ToList().ForEach(s => s.IsActiveEnrollment = false);
                    teacherToRemove.ToList().ForEach(s => s.SubjectId = null);
                }
            }
            await ctx.SaveChangesAsync();
            return id;

        }
        /// <summary>
        /// returns a teacher assignement to a subject
        /// </summary>
        /// <param name="teacherId">id of the teacher</param>
        /// <param name="subjectId">id of the subject</param>
        /// <returns>a teacher assignement to a subject</returns>
        public TeacherAssignement GetTeacherAssignement (int teacherId, int subjectId)
        {
            using var ctx = new UniversityDbContext();

            var teacherAssignement = ctx.TeacherAssignements.FirstOrDefault(s => s.SubjectId == subjectId && s.TeacherId == teacherId);
            if (teacherAssignement==null)
            {
                throw new InvalidIdException("invalid teacher assignement data");
            }
            return teacherAssignement;
        }

        /// <summary>
        /// removes a tescher assicnement
        /// </summary>
        /// <param name="teacherId">teaqxcher id</param>
        /// <param name="subjectiD">subject id</param>
        public void RemoveTeacherAssignement (int teacherId, int subjectiD)
        {
            using var ctx = new UniversityDbContext();

            var teacherAssignementToRemove = GetTeacherAssignement(teacherId, subjectiD); ;

            if (teacherAssignementToRemove == null)
            {
                throw new InvalidIdException("invalid teacher assignement data");
            }

            ctx.TeacherAssignements.Remove(teacherAssignementToRemove);

            ctx.SaveChanges();
        }
        /// <summary>
        /// creates a new teacher assignement to a subject
        /// </summary>
        /// <param name="assignement">teacher-subject assignement</param>
        /// <returns>assignement</returns>
        public async Task<TeacherAssignementsToGet> CreateTeacherAssignementAsync(TeacherAssignementsToCreate assignement)
        {
            using var ctx = new UniversityDbContext();

            var teacherAssignementToAdd = GetTeacherAssignement(assignement.TeacherId, assignement.SubjectId);

            var teacherAssignement = new TeacherAssignement
            {
                TeacherId = assignement.TeacherId,
                SubjectId = assignement.SubjectId
            };
            if (teacherAssignementToAdd == null)
            {
                ctx.TeacherAssignements.Add(teacherAssignement);
                await ctx.SaveChangesAsync();
                return teacherAssignement.ToDto();
            }
            else
            {
                //return Conflict("Teacher already assigned to this subject");
                return teacherAssignementToAdd.ToDto();
            }
            
        }
    }
}

