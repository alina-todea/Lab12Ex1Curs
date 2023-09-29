using System;
using DomainLayer.Models;
using CatalogAPI.DTOs;


namespace CatalogAPI.Extensions
{
	public static class StudentExtensions
	{

        /// <summary>
        /// this returns student data
        /// </summary>
        /// <param name="student">student's data</param>
        /// <returns>returns student data</returns>
        public static StudentToGet ToDto(this Student student )

        {
            if (student== null)
			{
				return null;
			}
			var addressToGet = student.Address.ToDto();

			return new StudentToGet
			{

				Id = student.Id,
				Name = student.Name,
				Age = student.Age,
				AddressToGet= addressToGet
			};
			

		}

   
    }
}

