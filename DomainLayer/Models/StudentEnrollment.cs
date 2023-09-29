using System;
using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Models
{
	public class StudentEnrollment
	{
        [Key]
        public int Id { get; set; }
        
        public int ? StudentId { get; set; }
        public Student ? Student { get; set; }
        public Subject ? Subject { get; set; }
        public int ? SubjectId { get; set; }

        public bool IsActiveEnrollment { get; set; } = true;
        public double AvgMark { get; set; } = 0.0;
    }
}

