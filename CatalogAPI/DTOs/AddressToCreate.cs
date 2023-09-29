using System;
using System.ComponentModel.DataAnnotations;

namespace CatalogAPI.DTOs


{
    /// <summary>
    /// this represents an address data
    /// </summary>
    public class AddressToCreate
	{
        /// <summary>
        /// city
        /// </summary>
        [Required(ErrorMessage ="City cannot be empty")]
        //[MinLength(1,ErrorMessage ="City caqnnot be empty")]
        public string City { get; set; }
        /// <summary>
        /// street
        /// </summary>
        [Required(ErrorMessage = "Street cannot be empty")]
        public string Street { get; set; }
        /// <summary>
        /// street number
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int Nr { get; set; }

    }
}

