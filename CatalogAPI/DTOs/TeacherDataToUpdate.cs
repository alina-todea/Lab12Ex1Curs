using System;
using System.ComponentModel.DataAnnotations;

namespace CatalogAPI.DTOs
{
	public class TeacherDataToUpdate
	{
        /// <summary>
        /// teacher name
        /// </summary>
        [Required(ErrorMessage = "Name cannot be empty")]
        public string Name { get; set; }
        /// <summary>
        /// teacher rank
        /// </summary>
        [Required]
        [RegularExpression("Instructor|Assistant Professor|Associate Professor|Professor", ErrorMessage = "Invalid Rank")]
        public string RankValue { get; set; }
    }
}

