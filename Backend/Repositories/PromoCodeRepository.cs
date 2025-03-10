using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using carwash.Data;
using carwash.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace carwash.Repositories
{
    public class PromoCodeRepository : IPromoCode
    {
        private readonly CarwashDbContext _db;
        public PromoCodeRepository(CarwashDbContext db)
        {
            _db=db;
        }
        public async Task<IEnumerable<PromoCode>> GetAllPromoCodesAsync()
        {
            var res = await _db.PromoCodes.ToListAsync();
            return res;
        }

        public async Task<PromoCode> GetPromoCodeByIdAsync(Guid id)
        {
            var res= await _db.PromoCodes.FirstOrDefaultAsync(x => x.Id == id);
            return res;
        }
        public async Task<PromoCode> CreatePromoCodeAsync(PromoCode obj)
        {
            await _db.PromoCodes.AddAsync(obj);
            await _db.SaveChangesAsync();
            return obj;
        }
        public async Task<PromoCode> DeletePromoCodeAsync(Guid id)
        {
            var res = await _db.PromoCodes.FirstOrDefaultAsync(x=>x.Id== id);
            if(res==null)
                return null;
            _db.PromoCodes.Remove(res);
            await _db.SaveChangesAsync();
            return res;
        }


    }
}