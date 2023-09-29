using System;
using CatalogAPI.DTOs;
using CatalogAPI.Filters;
using DomainLayer.Models;

namespace CatalogAPI.Extensions
{
	public static class AddressExtension
	{
        /// <summary>
        /// this returns address tata
        /// </summary>
        /// <param name="address"></param>
        /// <returns>returns address data</returns>
        public static AddressToGet ToDto(this Address address)
        {
            if (address == null)
            {
                return null;
            }
            return new AddressToGet
            {

                Id = address.Id,
                City = address.City,
                Street = address.Street,
                Nr = address.Nr
            };


        }

    }
}

