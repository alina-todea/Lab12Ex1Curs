using CatalogAPI.DTOs;
using DomainLayer.Models;

namespace CatalogAPI.Services
{
    public interface IAddressesService
    {
        Task<int> CreateAddressAsync(AddressToCreate addressToCreate);
        Task DeleteAddressAsync(int? id);
        bool ExistOtherOwners(int id, int? addressId);
        AddressToGet GetAddressByData(AddressToCreate addressToCheck);
        Address GetAddressById(int? id);
        List<AddressToDisplay> GetAllAddresses();
        Task<int> IdentifyOrCreateAddressAsync(AddressToCreate address);
        Task<int> IdentifyOrUpdateAddressAsync(int id, int? currentAddressId, AddressToCreate address);
        Task<int> UpdateAddressAsync(int? id, AddressToCreate addressToUpdate);
    }
}