using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using carwash.Models.Domain;

namespace carwash.Repositories
{
    public interface IAddress
    {
        Task<IEnumerable<Address>> GetAllAddressAsync();
        Task<Address> GetAddressByIdAsync(Guid id);
        Task<IEnumerable<Address>> GetAddressByCityAsync(string city);
        Task<Address> GetAddressByUserIdAsync(Guid userId);
        Task<Address> CreateAddressAsync(Address obj);
        Task<Address> UpdateAddressAsync(Guid id, Address obj);
        Task<Address> UpdateAddressByUserIdAsync(Guid userId, Address obj);
        Task<Address> DeleteAddressAsync(Guid id);
    }
}