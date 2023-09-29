using System;
using System.ComponentModel.DataAnnotations;

namespace CatalogAPI.DTOs
{
    /// <summary>
    /// this represents a student data
    /// </summary>
    public class StudentDataToUpdate
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
        
    }
}

