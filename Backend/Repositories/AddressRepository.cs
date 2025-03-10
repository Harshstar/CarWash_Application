using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using carwash.Data;
using carwash.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace carwash.Repositories
{
    public class AddressRepository : IAddress
    {
        private readonly CarwashDbContext _db;
        public AddressRepository(CarwashDbContext db)
        {
            _db=db;
        }
        public async Task<IEnumerable<Address>> GetAllAddressAsync()
        {
            var res = await _db.Addresses.ToListAsync();
            return res;
        }

        public async Task<Address> GetAddressByIdAsync(Guid id)
        {
            var res= await _db.Addresses.FirstOrDefaultAsync(x => x.Id == id);
            return res;
        }

        public async Task<IEnumerable<Address>> GetAddressByCityAsync(string city)
        {
            var res= await _db.Addresses.Include(x=>x.User).Where(x => x.City.ToLower() == city.ToLower()).ToListAsync();
            return res;
        }
        public async Task<Address> GetAddressByUserIdAsync(Guid userId)
        {
            var res= await _db.Addresses.FirstOrDefaultAsync(x => x.UserId == userId);
            return res;
        }

        public async Task<Address> CreateAddressAsync(Address obj)
        {
            await _db.Addresses.AddAsync(obj);
            await _db.SaveChangesAsync();
            return obj;
        }

        public async Task<Address> UpdateAddressAsync(Guid id, Address obj)
        {
            var res = await _db.Addresses.FirstOrDefaultAsync(x=>x.Id == id);
            if(res==null)
                return null;
            res.DoorNumber = obj.DoorNumber;
            res.Street = obj.Street;
            res.Area = obj.Area;
            res.Landmark = obj.Landmark;
            res.City = obj.City;
            res.State = obj.State;
            res.Pincode = obj.Pincode;
            await _db.SaveChangesAsync();
            return res;

        }
        public async Task<Address> UpdateAddressByUserIdAsync(Guid userId, Address obj)
        {
            var res = await _db.Addresses.FirstOrDefaultAsync(x=>x.UserId == userId);
            if(res==null)
                return null;
            res.DoorNumber = obj.DoorNumber;
            res.Street = obj.Street;
            res.Area = obj.Area;
            res.Landmark = obj.Landmark;
            res.City = obj.City;
            res.State = obj.State;
            res.Pincode = obj.Pincode;
            await _db.SaveChangesAsync();
            return res;

        }
        public async Task<Address> DeleteAddressAsync(Guid id)
        {
            var res = await _db.Addresses.FirstOrDefaultAsync(x=>x.Id== id);
            if(res==null)
                return null;
            _db.Addresses.Remove(res);
            await _db.SaveChangesAsync();
            return res;
        }

        public async Task<Address> DeleteAddressByCustomerId(Guid CustId)
        {
            var res = await _db.Addresses.FirstOrDefaultAsync(x=>x.Id== CustId);
            if(res==null)
                return null;
            _db.Addresses.Remove(res);
            await _db.SaveChangesAsync();
            return res;
        }
        public async Task<Address> DeleteAddressByWasherId(Guid WasherId)
        {
            var res = await _db.Addresses.FirstOrDefaultAsync(x=>x.Id== WasherId);
            if(res==null)
                return null;
            _db.Addresses.Remove(res);
            await _db.SaveChangesAsync();
            return res;
        }

    }
}