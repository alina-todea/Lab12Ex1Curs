using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using CatalogAPI.DTOs;
using CatalogAPI.Extensions;
using CatalogAPI.Filters;
using DomainLayer.Data;
using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Services
{
    public class AddressesService : IAddressesService


    {
        /// <summary>
        /// return address based on address id
        /// </summary>
        /// <param name="addressToCheck">AddressToCreate object</param>
        /// <returns>AddressToGet</returns>
        public AddressToGet GetAddressByData(AddressToCreate addressToCheck)
        {
            using var ctx = new UniversityDbContext();

            var existingAddress = ctx.Addresses
                .FirstOrDefault(a => a.City.ToLower().Replace(" ", "").Replace("-", "") == addressToCheck.City.ToLower().Replace(" ", "").Replace("-", "")
                  && a.Street.ToLower().Replace(" ", "").Replace("-", "") == addressToCheck.Street.ToLower().Replace(" ", "").Replace("-", "")
                  && a.Nr == addressToCheck.Nr);


            if (existingAddress == null)
            {
                return null;
            }
            else
            {
                return existingAddress.ToDto();
            }
        }
        /// <summary>
        /// verifies if other owners exist for the address, based on current owner id
        /// </summary>
        /// <param name="id">owner id</param>
        /// <param name="addressId">address is</param>
        /// <returns>true if there is another teacher or student at the address</returns>
        public bool ExistOtherOwners(int id, int? addressId)
        {
            if (addressId == null)
            {
                return false;
            }

            using var ctx = new UniversityDbContext();

            var existsOtherAdressTeachersOwwners = ctx.Teachers.Any(s => s.AddressId == addressId && s.Id != id);
            var existsOtherAdressStudentsOwwners = ctx.Students.Any(s => s.AddressId == addressId && s.Id != id);

            return existsOtherAdressTeachersOwwners || existsOtherAdressStudentsOwwners;
        }


        /// <summary>
        /// creates an address
        /// </summary>
        /// <param name="addressToCreate"> address data</param>
        /// <returns>address id</returns>
        public async Task<int> CreateAddressAsync(AddressToCreate addressToCreate)
        {
            using var ctx = new UniversityDbContext();

            Address newAddress;

            newAddress = new Address
            {
                City = addressToCreate.City,
                Street = addressToCreate.Street,
                Nr = addressToCreate.Nr,
            };
            ctx.Addresses.Add(newAddress);
            await ctx.SaveChangesAsync();

            return newAddress.Id;
        }


        /// <summary>
        /// updates an address based on address id. if address does not exist, it creates it
        /// </summary>
        /// <param name="id">address id</param>
        /// <param name="addressToUpdate">address data</param>
        /// <returns>address id</returns>
        public async Task<int> UpdateAddressAsync(int? id, AddressToCreate addressToUpdate)
        {
            using var ctx = new UniversityDbContext();

            if (id == null)
            {
                var addressId = await CreateAddressAsync(addressToUpdate);
                await ctx.SaveChangesAsync();
                return addressId;

            }
            else
            {
                var existingAddress = GetAddressById(id);

                if (existingAddress == null)
                {
                    var addressId = await CreateAddressAsync(addressToUpdate);
                    await ctx.SaveChangesAsync();
                    return addressId;
                }

                else
                {
                    existingAddress.City = addressToUpdate.City;
                    existingAddress.Street = addressToUpdate.Street;
                    existingAddress.Nr = addressToUpdate.Nr;
                    await ctx.SaveChangesAsync();
                    return existingAddress.Id;
                }
            }
        }
        /// <summary>
        /// deletes an address
        /// </summary>
        /// <param name="id">address id</param>
        public async Task DeleteAddressAsync(int? id)
        {
            if (id == null)
            {
                throw new InvalidIdException("address id  invalid");
            }
            using var ctx = new UniversityDbContext();

            var addressToDelete = await ctx.Addresses.FirstOrDefaultAsync(a => a.Id == id);

            if (addressToDelete == null)
            {
                throw new InvalidIdException("address id  invalid");
            }
            ctx.Addresses.Remove(addressToDelete);
            await ctx.SaveChangesAsync();
        }

        /// <summary>
        /// identifies the address by data or creates a new one if it doesn't exist
        /// </summary>
        /// <param name="address"> address data</param>
        /// <returns>address</returns>
        public async Task<int> IdentifyOrCreateAddressAsync(AddressToCreate address)
        {
            using var ctx = new UniversityDbContext();

            int addressId;
            AddressToGet existingAddress;

            existingAddress = GetAddressByData(address);

            if (existingAddress != null)
            {
                addressId = existingAddress.Id;
            }
            else
            {
                addressId = await CreateAddressAsync(address);
            }

            await ctx.SaveChangesAsync();
            return addressId;
        }

        /// <summary>
        /// if the owner has an address that is not owned by anyone else, it will be updated. if the current address owned by someone else, a new address will be created. If new address already exists, the teacher will receive the address id.
        /// </summary>
        /// <param name="id">id of the owner</param>
        /// <param name="currentAddressId"> current address id</param>
        /// <param name="address">new address data</param>
        /// <returns></returns>
        public async Task<int> IdentifyOrUpdateAddressAsync(int id, int? currentAddressId, AddressToCreate address)
        {
            using var ctx = new UniversityDbContext();

            if (currentAddressId==null )
            {
                throw new InvalidIdException("invalid current address id");
            }

            var existingNewAddress = GetAddressByData(address);
            

            var otherOwnersOfCurrentAddress = ExistOtherOwners(id, currentAddressId);

            int newAddressId;

            if (existingNewAddress != null)
            {
                newAddressId = existingNewAddress.Id;
            }
            else
            {
                if (currentAddressId == null || otherOwnersOfCurrentAddress)
                {
                    newAddressId = await CreateAddressAsync(address);
                }
                else
                {
                    newAddressId = await UpdateAddressAsync(currentAddressId, address);

                }
            }

            await ctx.SaveChangesAsync();
            return newAddressId;
        }
        /// <summary>
        /// returns an address by id
        /// </summary>
        /// <param name="id">address id</param>
        /// <returns>address</returns>
        public Address GetAddressById(int? id)
        {
            using var ctx = new UniversityDbContext();
            if (id == null)
            {
                return null;
            }

            Address myAddress;

            myAddress = ctx.Addresses.FirstOrDefault(s => s.Id == id);

            if (myAddress == null)
            {
                return null;
            }
            return myAddress;
        }
        /// <summary>
        /// Returns all addresses
        /// </summary>
        /// <returns>list of addresses</returns>
        public List<AddressToDisplay> GetAllAddresses()
        {
            using var ctx = new UniversityDbContext();
            var allAddresses = ctx.Addresses.Select(x => new AddressToDisplay(x)).ToList();
            return allAddresses;
        }
    }
}


