using System;
using DomainLayer.Data;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using DomainLayer.Models;
using CatalogAPI.DTOs;
using CatalogAPI.Extensions;
using CatalogAPI.Filters;

namespace CatalogAPI.Services
{
    public class SubjetcsService : ISubjetcsService
    {
        /// <summary>
        /// returns a subject by id
        /// </summary>
        /// <param name="id">subject id</param>
        /// <returns>subject</returns>
        public Subject GetSubjectById([Range(1, int.MaxValue)] int id)
        {

            using var ctx = new UniversityDbContext();

            var subject = ctx.Subjects.FirstOrDefault(s => s.Id == id);
            if (subject==null)
            {
                throw new InvalidIdException("invalid subject id");
            }

            return subject;
        }
        /// <summary>
        /// returns subject by name
        /// </summary>
        /// <param name="subjectName">subject's name</param>
        /// <returns>subject</returns>
        public Subject GetSubjectByName(string subjectName)
        {

            using var ctx = new UniversityDbContext();

            var subject = ctx.Subjects.FirstOrDefault(s => s.Name.ToLower().Replace(" ", "") == subjectName.ToLower().Replace(" ", ""));

            if (subject == null)
            {
                return null;             } 

            return subject;
        }

        /// <summary>
        /// creates a new subject
        /// </summary>
        /// <param name="subjectToCreate">subject data</param>
        /// <returns>subject</returns>
        public Subject CreateSubject(SubjectToCreate subjectToCreate)
        {
            using var ctx = new UniversityDbContext();

            var existingSubject = ctx.Subjects.FirstOrDefault(s => s.Name.ToLower().Replace(" ", "") == subjectToCreate.Name.ToLower().Replace(" ", ""));

            if (existingSubject != null)
            {
                return existingSubject;
            }

            var subject = new Subject

            {
                Name = subjectToCreate.Name
            };

            ctx.Subjects.Add(subject);
            ctx.SaveChanges();

            return subject;
        }
        /// <summary>
        /// removes a subject
        /// </summary>
        /// <param name="subject">subject to remove</param>
        /// <returns></returns>
        /// <exception cref="InvalidIdException">invalid subject id</exception>
        public async Task RemoveSubjectAsync(Subject subject)
        {
            using var ctx = new UniversityDbContext();
            var existingSubject = GetSubjectById(subject.Id);


            if (existingSubject==null)
            {
                throw new InvalidIdException("invalid subject id");
            }
                ctx.Subjects.Remove(subject);
                await ctx.SaveChangesAsync();
                return;
            
        }
    }
}

