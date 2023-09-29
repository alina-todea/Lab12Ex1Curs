using System;
using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Models


{
	public class Subject
	{
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<TeacherAssignement> TeacherAssignement { get; set; } = new List<TeacherAssignement>();
        public ICollection<StudentEnrollment> StudentEnrollment { get; set; } = new List<StudentEnrollment>();



    }
}

