using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using carwash.Data;
using carwash.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace carwash.Repositories
{
    public class PaymentRepository : IPayment
    {
        private readonly CarwashDbContext _db;
        public PaymentRepository(CarwashDbContext db)
        {
            _db=db;
        }
        public async Task<IEnumerable<Payment>> GetAllPaymentAsync()
        {
            var res = await _db.Payments.ToListAsync();
            return res;
        }
        public async Task<Payment> CreatePayemntAsync(Payment obj)
        {
            await _db.Payments.AddAsync(obj);
            await _db.SaveChangesAsync();
            return obj;
        }

        public async Task<Payment> DeletePaymentAsync(Guid id)
        {
            var res = await _db.Payments.FirstOrDefaultAsync(x=>x.Id== id);
            if(res==null)
                return null;
            _db.Payments.Remove(res);
            await _db.SaveChangesAsync();
            return res;
        }

    }
}