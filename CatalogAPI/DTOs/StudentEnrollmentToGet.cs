using System;
using DomainLayer.Models;

namespace CatalogAPI.DTOs
{
    /// <summary>
    /// this represents a student erollment in a subject
    /// </summary>
	public class StudentEnrollmentToGet
	{
        /// <summary>
        /// enrollment id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// status of the enrollment: true if student currently enrolled in the subject
        /// </summary>
        public bool IsActiveEnrollment { get; set; }
        /// <summary>
        /// average mark per student per subject
        /// </summary>
        public double AvgMark { get; set; }
        /// <summary>
        /// student id
        /// </summary>
        public int ? StudentId { get; set; }
        /// <summary>
        /// subject id
        /// </summary>
        public int ? SubjectId { get; set; }
        public SubjectToGet ? SubjectToGet { get; set; }

    }
}

