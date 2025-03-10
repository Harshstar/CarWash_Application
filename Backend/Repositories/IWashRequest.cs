using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using carwash.Models.Domain;

namespace carwash.Repositories
{
    public interface IWashRequest
    {
        Task<IEnumerable<WashRequest>> GetAllWashRequestAsync();
        Task<WashRequest> GetWashRequestByIdAsync(Guid id);
        Task<WashRequest> GetLatestWashRequestByCustIdAsync(Guid customerId);
        Task<IEnumerable<WashRequest>> GetCustomerWashRequests(Guid customerId);
        Task<IEnumerable<WashRequest>> GetWashRequestBywasherIdAsync(Guid washerId);
        Task<bool> IsWasherAvailable(Guid washerId, DateTime pickupDate);
        Task<WashRequest> CreateWashRequestAsync(WashRequest obj);
        Task<WashRequest> UpdateWashRequestAsync(Guid id, WashRequest obj);
        Task<WashRequest> DeleteWashRequestAsync(Guid id);

        Task<User> WasherDetails(Guid id);
    }
}