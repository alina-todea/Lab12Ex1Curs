using System;
using System.ComponentModel.DataAnnotations;
using DomainLayer.Models;


namespace CatalogAPI.DTOs
{
    /// <summary>
    /// this represents a student data
    /// </summary>
    public class StudentToGet
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
        /// <summary>
        /// student address
        /// </summary>
        public AddressToGet ? AddressToGet {get; set; }
    }
}

