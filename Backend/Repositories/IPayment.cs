using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using carwash.Models.Domain;

namespace carwash.Repositories
{
    public interface IPayment
    {
        Task <IEnumerable<Payment>> GetAllPaymentAsync(); 
        Task <Payment> CreatePayemntAsync(Payment obj);
        Task <Payment> DeletePaymentAsync(Guid id);
    }
}