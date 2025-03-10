using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using carwash.Data;
using carwash.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace carwash.Repositories
{
    public class WashRequestRepository : IWashRequest
    {
        private readonly CarwashDbContext _db;
        public WashRequestRepository(CarwashDbContext db)
        {
            _db=db;
        }

        public async Task<IEnumerable<WashRequest>> GetAllWashRequestAsync()
        {
            var res = await _db.WashRequests.Include(x=>x.Address).ToListAsync();
            return res;
        }

        public async Task<WashRequest> GetWashRequestByIdAsync(Guid id)
        {
            var res= await _db.WashRequests.Include(x=> x.Address).FirstOrDefaultAsync(x => x.Id == id);
            return res;
        }
        public async Task<WashRequest> GetLatestWashRequestByCustIdAsync(Guid customerId)
        {
            var res = await _db.WashRequests.Include(x=> x.Address).OrderByDescending(x => x.OrderedDate).FirstOrDefaultAsync(x => x.CustId == customerId);
            return res;
        }
        public async Task<IEnumerable<WashRequest>> GetCustomerWashRequests(Guid customerId)
        {
            var res = await _db.WashRequests.Where(x=>x.CustId == customerId).Include(x=>x.Washer).Include(x=>x.Car).Include(x=>x.PackageNavigation).ToListAsync();
            return res;
        }

        public async Task<IEnumerable<WashRequest>> GetWashRequestBywasherIdAsync(Guid washerId)
        {
            var res= await _db.WashRequests.Include(x=> x.Address).Where(x => x.WasherId == washerId).ToListAsync();
            return res;
        }

        public async Task<bool> IsWasherAvailable(Guid washerId, DateTime pickupDate)
        {
            int maxRequestsPerDay = 3;
            var washCount = await _db.WashRequests.Where(w => w.WasherId == washerId && w.PickupDate.Date == pickupDate.Date).CountAsync();
            if(washCount < maxRequestsPerDay)
                return true;
            return false;
        }
        public async Task<WashRequest> CreateWashRequestAsync(WashRequest obj)
        {
            await _db.WashRequests.AddAsync(obj);
            await _db.SaveChangesAsync();
            return obj;
        }

        public async Task<WashRequest> UpdateWashRequestAsync(Guid id, WashRequest obj)
        {
            var res = await _db.WashRequests.FirstOrDefaultAsync(x=>x.Id == id);
            if(res==null)
                return null;
            res.CarId=obj.CarId;
            res.CustId=obj.CustId;
           // exsWashReq.Location=washRequest.Location;
            res.Notes=obj.Notes;
            res.PackageId=obj.PackageId;
            res.DeliveryDate=obj.DeliveryDate;
            res.OrderedDate=obj.OrderedDate;
            res.PickupDate=obj.PickupDate;
            res.WasherId=obj.WasherId;
            res.WashType=obj.WashType;
            res.AddressId = obj.AddressId;
            await _db.SaveChangesAsync();
            return obj;
        }

        public async Task<User> WasherDetails(Guid id)
        {
            var washer=await _db.Users.FirstOrDefaultAsync(x=>x.Id==id);
            return washer;
        }
        public async Task<WashRequest> DeleteWashRequestAsync(Guid id)
        {
            var res = await _db.WashRequests.FirstOrDefaultAsync(x=>x.Id== id);
            if(res==null)
                return null;
            _db.WashRequests.Remove(res);
            await _db.SaveChangesAsync();
            return res;
        }
    }
}  