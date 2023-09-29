using System;
using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Models
{
	public class TeacherAssignement
	{
        [Key]
        public int Id { get; set; }

        public int ? TeacherId { get; set; }
        public Teacher ? Teacher { get; set; }
        public Subject ? Subject { get; set; }
        public int ? SubjectId { get; set; }
    }
}


