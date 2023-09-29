using System;
using System.ComponentModel.DataAnnotations;
using DomainLayer.Models;

namespace CatalogAPI.DTOs
{
   /// <summary>
   /// this represents a student data
   /// </summary>
	public class StudentToCreate
	{
        /// <summary>
        /// student name
        /// </summary>
        [Required(ErrorMessage = "Name cannot be empty")]
        public string Name { get; set; }
        /// <summary>
        /// student age
        /// </summary>
        [Required]
        [Range(18, 150)]
        public int Age { get; set; }
        /// <summary>
        /// student address
        /// </summary>
        [Required(ErrorMessage = "Address cannot be empty")]
        public AddressToCreate AddressToCreate { get; set; }
       





    }

}

