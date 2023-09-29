using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Models
{
	public class Teacher
	{
        [Key]
        public int Id { get; set; }
        public string Name { get; set;}
        public string RankValue { get; set; }


        public int ? AddressId { get; set; }
        public virtual Address ? Address { get; set; }

        public ICollection<TeacherAssignement> TeacherAssignements { get; set; } = new List<TeacherAssignement>();
        //public List<Mark> ListOfMarks { get; set; }
    }
}

