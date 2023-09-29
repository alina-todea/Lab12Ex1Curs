using System;
using System.ComponentModel.DataAnnotations;

namespace CatalogAPI.DTOs
{
	
        /// <summary>
        /// this represents a teacher's data
        /// </summary>
        public class TeacherNoRankToUpdate
        {
            /// <summary>
            /// teacher name
            /// </summary>
            [Required(ErrorMessage = "Name cannot be empty")]
            public string Name { get; set; }
            /// <summary>
            /// teachewr address
            /// </summary>
            [Required(ErrorMessage = "Address cannot be empty")]
            public AddressToCreate AddressToCreate { get; set; }
        }
}

