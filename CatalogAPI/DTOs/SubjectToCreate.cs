using System;
using System.ComponentModel.DataAnnotations;
using DomainLayer.Models;

namespace CatalogAPI.DTOs
{
    /// <summary>
    /// this represents a dubject data
    /// </summary>
    public class SubjectToCreate
	{
        /// <summary>
        /// subject name
        /// </summary>
        [Required(ErrorMessage = "Name cannot be empty")]
        public string Name { get; set; }
    }
}

