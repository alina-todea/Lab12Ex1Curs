using System;
using DomainLayer.Models;
using System.Diagnostics.Metrics;

namespace CatalogAPI.DTOs
{
    /// <summary>
    /// this represents student data
    /// </summary>
    public class StudentDataToGet
	{
        /// <summary>
        /// student id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// student name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// student age
        /// </summary>
        public int Age { get; set; }

      
        public StudentDataToGet(Student x)
        {
            Id = x.Id;
            Name = x.Name;
            Age = x.Age;
        }
    }
}

