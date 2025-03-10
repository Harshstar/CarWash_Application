using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using carwash.Data;
using carwash.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace carwash.Repositories
{
    public class CarRepository : ICar
    {
        public CarwashDbContext _db;
        public CarRepository(CarwashDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Car>> GetAllCarsAsync()
        {
            var res = await _db.Cars.Include(x=>x.Customer).ToListAsync();
            return res;
        }
        
        public async Task<Car> GetCarByIdAsync(Guid id)
        {
            var res = await _db.Cars.Include(x=>x.Customer).FirstOrDefaultAsync(x=>x.Id == id);
            return res;
        }

        public async Task<IEnumerable<Car>> GetCarsByCustomerIdAsync(Guid customerId)
        {
            var res = await _db.Cars.Include(x=>x.Customer).Where(x=>x.CustId==customerId).ToListAsync();
            return res;
        }
        public async Task<Car> AddCarAsync(Car obj)
        {
            await _db.Cars.AddAsync(obj);
            await _db.SaveChangesAsync();
            return obj;
        }

        public async Task<Car> UpdateCarAsync(Guid id, Car obj)
        {
            var res = await _db.Cars.FirstOrDefaultAsync(x=>x.Id == id);
            if(res==null)
                return null;
            res.CustId = obj.CustId;
            res.Number = obj.Number;
            res.Company = obj.Company;
            res.Model = obj.Model;
            await _db.SaveChangesAsync();
            return res;
        }
        public async Task<Car> DeleteCarAsync(Guid carId)
        {
            var res = await _db.Cars.FirstOrDefaultAsync(x=>x.Id== carId);
            if(res==null)
                return null;
            var activeWashRequest = await _db.WashRequests.AnyAsync(w => w.CarId == carId && w.DeliveryDate > DateTime.UtcNow);

            if (activeWashRequest)
            {
                throw new InvalidOperationException("Car cannot be deleted as it is linked to an active wash request.");
            }
        
            
            var completedWashRequests = await _db.WashRequests.Where(w => w.CarId == carId && w.DeliveryDate <= DateTime.UtcNow).ToListAsync();

            if (completedWashRequests.Any())
            {
                _db.WashRequests.RemoveRange(completedWashRequests);
                await _db.SaveChangesAsync();
            }
            
            _db.Cars.Remove(res);
            await _db.SaveChangesAsync();
            return res;
        }
    }
}