using System;
using System.ComponentModel.DataAnnotations;
using CatalogAPI.DTOs;
using CatalogAPI.Filters;
using DomainLayer.Data;
using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Services
{
    public class MarksService : IMarksService
    {
        /// <summary>
        /// calculates average mark
        /// </summary>
        /// <param name="markToCreate">mark data</param>
        public void SaveAverageMarkStudentSubject(MarkToCreate markToCreate)
        {
            using var ctx = new UniversityDbContext();
            var averageMark = ctx.Marks
                .Where(m => m.StudentId == markToCreate.StudentId && m.SubjectId == markToCreate.SubjectId)
                .Average(m => m.Value);

            var studentEnrollmentToUpdate = ctx.StudentEnrollments.FirstOrDefault(s => s.SubjectId == markToCreate.SubjectId && s.StudentId == markToCreate.StudentId);

            studentEnrollmentToUpdate.AvgMark = averageMark;
            ctx.SaveChangesAsync();

            return;
        }
        /// <summary>
        /// disables a mark record by setting subject, teacher or student id to null
        /// </summary>
        /// <param name="id">the id of the item to be deleted</param>
        /// <param name="type">subject or teacheritem that is deketed: student, </param>
        /// <returns></returns>
        public async Task<int> DisableMarkRecordByIdAsync(int id, [RegularExpression("student|teacher|subject", ErrorMessage = "Invalid Rank")] string type)
        {
            using var ctx = new UniversityDbContext();

            if (type == "student")
            {
                var marks = ctx.Marks.Where(s => s.StudentId == id).ToList();
                if (marks==null)
                {
                    throw new InvalidIdException("inexistent student mark");
                }
                marks.ToList().ForEach(s => s.StudentId = null);
            }
            else if (type == "teacher")
            {
                var marks = ctx.Marks.Where(s => s.TeacherId == id).ToList();
                if (marks == null)
                {
                    throw new InvalidIdException("inexistent teacher mark");
                }
                marks.ToList().ForEach(s => s.TeacherId = null);
            }
            else if (type == "subject")
            {
                var marks = ctx.Marks.Where(s => s.SubjectId == id).ToList();
                if (marks == null)
                {
                    throw new InvalidIdException("inexistent subject mark");
                }
                marks.ToList().ForEach(s => s.SubjectId = null);
            }
            await ctx.SaveChangesAsync();
            return id;
        }
        /// <summary>
        /// returns the marks of a student by id, for a subject 
        /// </summary>
        /// <param name="id">id of the student</param>
        /// <param name="subjectName">name of the subject</param>
        /// <returns></returns>
        public List<MarkToDisplay> GetMarkByStudentId(int id, string subjectName)


        {
            using var ctx = new UniversityDbContext();

            if (subjectName == "")
            {
                var allMarks = ctx.Marks.Where(s => s.StudentId == id).Select(x => new MarkToDisplay(x))
                   .ToList();
                return allMarks;
            }
            else
            {
                var subject = ctx.Subjects.FirstOrDefault(s => s.Name.ToLower().Replace(" ", "") == subjectName.ToLower().Replace(" ", ""));

                if (subject == null)
                {
                    var allMarks = ctx.Marks.Where(s => s.StudentId == id).Select(x => new MarkToDisplay(x))
                   .ToList();
                    return allMarks;
                }
                else
                {
                    var allMarks = ctx.Marks.Where(s => s.SubjectId == subject.Id && s.StudentId == id).Select(x => new MarkToDisplay(x))
                        .ToList();
                    return allMarks;
                }

            }
        }
        /// <summary>
        /// returns the average marks for a student
        /// </summary>
        /// <param name="id">id of the student</param>
        /// <returns>list of average marks</returns>
        public List<AverageMarkToGet> GetAvgMarkByStudentId(int id)
        {
            using var ctx = new UniversityDbContext();

            var allAvgMarks = ctx.StudentEnrollments.Where(s => s.StudentId == id).Select(x => new AverageMarkToGet(x))
                .ToList();
            return allAvgMarks;
        }
        /// <summary>
        /// creates a new mark
        /// </summary>
        /// <param name="markToCreate">mark data</param>
        /// <returns>mark</returns>
        public async Task<Mark> GiveMarkAsync(MarkToCreate markToCreate)
        {
            using var ctx = new UniversityDbContext();

            var newMark = new Mark
            {
                Value = markToCreate.Value,
                StudentId = markToCreate.StudentId,
                TeacherId = markToCreate.TeacherId,
                SubjectId = markToCreate.SubjectId,
                Date = DateTime.UtcNow
            };
            ctx.Marks.Add(newMark);
            await ctx.SaveChangesAsync();

            SaveAverageMarkStudentSubject(markToCreate);
            await ctx.SaveChangesAsync();

            return newMark;
        }
        /// <summary>
        /// returns all marks
        /// </summary>
        /// <returns>list of marks</returns>
        public List<MarkToDisplay>GetAllMarks()
        {
         using var ctx = new UniversityDbContext();

         var allmarks = ctx.Marks.Select(x => new MarkToDisplay(x)).ToList();

         return allmarks;
         }
    }
}

