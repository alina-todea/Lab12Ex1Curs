using System;
using System.ComponentModel.DataAnnotations;

namespace CatalogAPI.DTOs
{
    /// <summary>
    /// this repressents a student enrollmemnt in a subject
    /// </summary>
    public class StudentEnrollmentToCreate
	{
        /// <summary>
        /// student id
        /// </summary>
        [Required]
        public int StudentId { get; set; }
        /// <summary>
        /// subject id
        /// </summary>
        [Required]
        public int SubjectId { get; set; }
        /// <summary>
        /// status of the enrollment: true if student currently enrolled in the subject
        /// </summary>
       /* [Required]
        public bool IsActiveEnrollment { get; set; } = false;
        /// <summary>
        /// average mark per student per subject
        /// </summary>
        [Required]
        [Range(0.0, double.MaxValue)]
        public double AvgMark { get; set; } = 0.0;*/
    }
	
}

