using System;
using CatalogAPI.DTOs;
using DomainLayer.Models;

namespace CatalogAPI.Extensions
{
	public static class SubjectExtension
	{
        /// <summary>
        /// this returns subject data
        /// </summary>
        /// <param name="subject">subject's data</param>
        /// <returns>returns subject data</returns>
        public static SubjectToGet ToDto(this Subject subject)

        {
            if (subject == null)
            {
                return null;
            }

            return new SubjectToGet
            {

                Id = subject.Id,
                Name = subject.Name,
            };


        }
    }
}

