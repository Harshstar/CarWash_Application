using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using carwash.Models.Domain;

namespace carwash.Repositories
{
    public interface ICar
    {
        Task<IEnumerable<Car>> GetAllCarsAsync();
        Task<Car> GetCarByIdAsync(Guid id);
        Task<IEnumerable<Car>> GetCarsByCustomerIdAsync(Guid customerId);
        Task<Car> AddCarAsync(Car obj);
        Task<Car> UpdateCarAsync(Guid id, Car obj);
        Task<Car> DeleteCarAsync(Guid carId);
    }
}