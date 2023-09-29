using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Models
{
	public class Mark
	{
        [Key]
        public int Id { get; set; }
        public int Value { get; set; }

        public Subject ? Subject { get; set; }
        public int ? SubjectId { get; set; }
        public Student ? Student { get; set; }
        public int ? StudentId { get; set; }
        public Teacher ? Teacher { get; set; }
        public int ? TeacherId { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        [NotMapped]
        public double AvgStudentSubject { get; set; }
        



    }
}

