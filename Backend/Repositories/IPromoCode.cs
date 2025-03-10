using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using carwash.Models.Domain;

namespace carwash.Repositories
{
    public interface IPromoCode
    {
        Task<IEnumerable<PromoCode>> GetAllPromoCodesAsync();
        Task<PromoCode> GetPromoCodeByIdAsync(Guid id);
        Task<PromoCode> CreatePromoCodeAsync(PromoCode obj);
        Task<PromoCode> DeletePromoCodeAsync(Guid id);
    }
}