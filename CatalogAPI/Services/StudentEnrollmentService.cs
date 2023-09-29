using System;
using System.ComponentModel.DataAnnotations;
using CatalogAPI.DTOs;
using CatalogAPI.Extensions;
using CatalogAPI.Filters;
using DomainLayer.Data;
using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Services
{
    public class StudentEnrollmentService : IStudentEnrollmentService
    {
        /// <summary>
        /// disables the enrollment of a student in a subject, by setting the IsActiveEnrollment to false and the student id to null. Used when a student is deleted
        /// </summary>
        /// <param name="id">id of the item (student or subject)</param>
        /// <param name="type">student or subject</param>
        /// <returns>id of the item (student or subject)</returns>
        public async Task<int> RemoveStudentFromSubjectByIdAsync(int id, [RegularExpression("student|subject", ErrorMessage = "Invalid Rank")] string type)

        {
            using var ctx = new UniversityDbContext();
            if (type == "student")
            {

                var subjectStudentRels = ctx.StudentEnrollments.Where(s => s.StudentId == id).ToList();
                if (subjectStudentRels==null)
                {
                    throw new InvalidIdException("inexistent enrollment");
                }

                    subjectStudentRels.ToList().ForEach(s => s.IsActiveEnrollment = false);
                    subjectStudentRels.ToList().ForEach(s => s.StudentId = null);
            }
            else if (type == "subject")
            {

                var subjectStudentRels = ctx.StudentEnrollments.Where(s => s.SubjectId == id).ToList();
                if (subjectStudentRels == null)
                {
                    throw new InvalidIdException("inexistent enrollment");
                }
                    subjectStudentRels.ToList().ForEach(s => s.IsActiveEnrollment = false);
                    subjectStudentRels.ToList().ForEach(s => s.SubjectId = null);
            }
            await ctx.SaveChangesAsync();
            return id;
        }

        /// <summary>
        /// return a student enrollment by student and subject ids
        /// </summary>
        /// <param name="studentId">student id</param>
        /// <param name="subjectId">subject id</param>
        /// <returns>student enrollment</returns>
        public StudentEnrollment GetStudentEnrollment(int studentId, int subjectId)

        {
            using var ctx = new UniversityDbContext();

            var studentEnrollment = ctx.StudentEnrollments
                .FirstOrDefault(s => s.SubjectId == subjectId && s.StudentId == studentId);
            
            return studentEnrollment;
        }

        /// <summary>
        /// enrolls a student in a subject if not enrolled, and if enrolled and enrollment is inactive, it activates it. 
        /// </summary>
        /// <param name="studentId"> student id</param>
        /// <param name="subjectId">subject id</param>
        /// <returns>student enrollment</returns>
        public async Task<StudentEnrollment> EnrollStudentAsync(int studentId, int subjectId)
        {
            using var ctx = new UniversityDbContext();

            var studentEnrollmentToAdd =  GetStudentEnrollment(studentId, subjectId);

            var studentEnrollment = new StudentEnrollment
            {
                StudentId = studentId,
                SubjectId = subjectId,
                IsActiveEnrollment = true,
                AvgMark = 0
            };

            if (studentEnrollmentToAdd == null)
            {
                ctx.StudentEnrollments.Add(studentEnrollment);
                await ctx.SaveChangesAsync();
                return studentEnrollment;
            }
            else
            {
                if (studentEnrollmentToAdd.IsActiveEnrollment == true)
                {
                    throw new DuplicateException("student already enrolled");
                }
                else
                {
                   studentEnrollmentToAdd.IsActiveEnrollment = true;
                    await ctx.SaveChangesAsync();
                    return studentEnrollmentToAdd;
                }
            }
            
        }

        /// <summary>
        /// inactivates a student enrollment if exists and it is active
        /// </summary>
        /// <param name="studentId">student id</param>
        /// <param name="subjectId">subject id</param>
        public  async Task<int> InactivateStudentEnrollmentAsync(int studentId, int subjectId)
        {
            using var ctx = new UniversityDbContext();

            var studentEnrollmentToRemove =  GetStudentEnrollment(studentId, subjectId);

            if (studentEnrollmentToRemove == null)
            {
                throw new InvalidIdException("inexistent  enrollment");
            }
            else if (studentEnrollmentToRemove.IsActiveEnrollment == false)
            {
                throw new InvalidIdException("inactive enrollment");
            }
            else
            {
                studentEnrollmentToRemove.IsActiveEnrollment = false;
            }

             await ctx.SaveChangesAsync();
             return studentId;
        }

    }
}
