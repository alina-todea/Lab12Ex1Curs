using System;
using DomainLayer.Models;

namespace CatalogAPI.DTOs
{
    /// <summary>
    /// this represents a subject data
    /// </summary>
    public class SubjectToGet
	{
        /// <summary>
        /// subject id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// subject name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// teachers data
        /// </summary>
        public  TeacherToGet Teacher { get; set; }
    }
}

