using System;
using DomainLayer.Models;
using System.ComponentModel.DataAnnotations;

namespace CatalogAPI.DTOs
{
    /// <summary>
    /// this represents a mark data
    /// </summary>
    public class MarkToCreate
	{
       
        
        /// <summary>
        /// mark value 1-10
        /// </summary>
        [Range(1, 10)]
        [Required]
        public int Value { get; set; }
        /// <summary>
        /// subject id
        /// </summary>
        [Range(1,   int.MaxValue)]
        [Required]
        public int SubjectId { get; set; }
        /// <summary>
        /// student id
        /// </summary>
        [Range(1, int.MaxValue)]
        [Required]
        public int StudentId { get; set; }
        /// <summary>
        /// teacher id
        /// </summary>
        [Range(1, int.MaxValue)]
        [Required]
        public int TeacherId { get; set; }
        
        
    }
}

