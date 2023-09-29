using System;
using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Models
{
	public class Address
	{

        [Key]
        public int Id { get; set; }
        /// <summary>
        /// string City
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// string Street
        /// </summary>
        public string Street { get; set; }
        /// <summary>
        /// int Nr
        /// </summary>
        public int Nr { get; set; }


        public override string ToString() => $"{City}|{Street}|{Nr}";

    }
}

