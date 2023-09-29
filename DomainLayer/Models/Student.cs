using System;
using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Models
{
	
	public class Student
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public Address ? Address { get; set; }
        public int ? AddressId { get; set; }
        //public List<Mark> ListOfMarks { get; set; }

        public ICollection<StudentEnrollment> StudentEnrollment { get; set; } = new List<StudentEnrollment>();


        //public override string ToString() => $"{First}|{Last}|{Age}|{Address}";

    }
}

