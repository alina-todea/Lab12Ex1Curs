using System;
using DomainLayer.Models;

namespace CatalogAPI.DTOs
{
    /// <summary>
    /// this represents an address data
    /// </summary>
    public class AddressToGet
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
    }
}

