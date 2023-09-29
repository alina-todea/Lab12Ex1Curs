using System;
using System.ComponentModel.DataAnnotations;

namespace CatalogAPI.DTOs
{
    /// <summary>
    /// this represents a teacher data
    /// </summary>
    public class TeacherToUpdate
	{
        /// <summary>
        /// teacher name
        /// </summary>
        [Required(ErrorMessage = "Name cannot be empty")]
        public string Name { get; set; }
        /// <summary>
        /// teacher city
        /// </summary>
        [Required(ErrorMessage = "City cannot be empty")]
        public string City { get; set; }
        /// <summary>
        /// teacher street
        /// </summary>
        [Required(ErrorMessage = "Street cannot be empty")]
        public string Street { get; set; }
        /// <summary>
        /// teacher street number
        /// </summary>
        [Required]
        [Range(1,int.MaxValue)]
        public int Nr { get; set; }
        /// <summary>
        /// subject id
        /// </summary>
        public int SubjectId { get; set; }

    }
}


