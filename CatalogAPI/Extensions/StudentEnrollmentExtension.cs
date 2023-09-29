using System;
using CatalogAPI.DTOs;
using DomainLayer.Models;

namespace CatalogAPI.Extensions
{
	public static class StudentEnrollmentExtension
	{
        /// <summary>
        /// ths returns a student's enrollment in a subject
        /// </summary>
        /// <param name="studentEnrollment">student enrollment's data</param>
        /// <returns>returns enrollment</returns>
        public static StudentEnrollmentToGet ToDto(this StudentEnrollment studentEnrollment)

        {
            if (studentEnrollment == null)
            {
                return null;
            }
           
            return new StudentEnrollmentToGet
            {
                StudentId = studentEnrollment.StudentId,
                SubjectId = studentEnrollment.SubjectId,
                IsActiveEnrollment= studentEnrollment.IsActiveEnrollment

            };
        }
    }
}

