using System;
using CatalogAPI.DTOs;
using DomainLayer.Models;

namespace CatalogAPI.Extensions
{
	public static class TeacherAssignementsExtensions
	{
        /// <summary>
        /// this returns a teacher's assignement to a subject
        /// </summary>
        /// <param name="teacherAssignement">teacher assignement's data</param>
        /// <returns>returns teacher assignement in a subject </returns>
        public static TeacherAssignementsToGet ToDto(this TeacherAssignement teacherAssignement)
        {
            if (teacherAssignement == null)
            {
                return null;
            }
            return new TeacherAssignementsToGet
            {

                SubjectId = teacherAssignement.SubjectId,
                TeacherId = teacherAssignement.TeacherId,

            };


        }
    }
}

