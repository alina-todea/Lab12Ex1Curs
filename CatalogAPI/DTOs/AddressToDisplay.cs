using System;
using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Linq;

namespace CatalogAPI.DTOs
{
    /// <summary>
    /// address data
    /// </summary>
    public class AddressToDisplay
	{
		
        /// <summary>
        /// address id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// city
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// street
        /// </summary>
        public string Street { get; set; }
        /// <summary>
        /// street number
        /// </summary>
        public int Nr { get; set; }

        public AddressToDisplay(Address x)
        {
            City = x.City;
            Street = x.Street;
            Nr = x.Nr;
        }
    }
}


