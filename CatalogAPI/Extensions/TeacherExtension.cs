using System;
using CatalogAPI.DTOs;
using CatalogAPI.Filters;
using DomainLayer.Models;

namespace CatalogAPI.Extensions
{
	public static class TeacherExtension
	{
        /// <summary>
        /// this returns a teacher
        /// </summary>
        /// <param name="teacher">teacher's data</param>
        /// <returns>teacher DTO</returns>
        public static TeacherToGet ToDto(this Teacher teacher)

        {
            if (teacher == null)
            {
                return null;
            }

            var addressToGet = teacher.Address.ToDto();
           

            return new TeacherToGet
            {

                Id = teacher.Id,
                Name = teacher.Name,
                Rankvalue = teacher.RankValue,
                AddressToGet = addressToGet
                


            };

        }
    }

}

