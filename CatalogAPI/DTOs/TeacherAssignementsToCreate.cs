using System;
using System.ComponentModel.DataAnnotations;

namespace CatalogAPI.DTOs
{
    /// <summary>
    /// this represents a teacher assignement to a subject
    /// </summary>
    public class TeacherAssignementsToCreate
	{
        /// <summary>
        /// teacher id
        /// </summary>
        [Required]
        public int TeacherId { get; set; }
        /// <summary>
        /// subject id
        /// </summary>
        [Required]
        public int SubjectId { get; set; }
    }
}

