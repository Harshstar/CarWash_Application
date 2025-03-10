using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using carwash.Data;
using carwash.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace carwash.Repositories
{
    public class UserRepository : IUser
    {
        private readonly CarwashDbContext _db;
        public UserRepository(CarwashDbContext db)
        {
            _db=db;
        }
        public async Task<List<User>> GetAllUserAsync()
        {
            var res = await _db.Users.Include(x=>x.Cars).ToListAsync();
            return res;
        }
        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            var res= await _db.Users.Include(x=>x.Cars).FirstOrDefaultAsync(x => x.Id == userId);
            return res;
        }
        public async Task<User> AddUserAsync(User obj)
        {    
            await _db.Users.AddAsync(obj);
            await _db.SaveChangesAsync();
            return obj;
        }
        public async Task<User> UpdateUserAsync(Guid id, User obj)
        {
            var res = await _db.Users.FirstOrDefaultAsync(x=>x.Id == id);
            if(res==null)
                return null;
            res.Id = obj.Id;
            res.Name = obj.Name;
            res.Email = obj.Email;
            res.MobileNumber = obj.MobileNumber;
            await _db.SaveChangesAsync();
            return res;
        }
        public async Task<User> DeleteUserAsync(Guid id)
        {
            var res = await _db.Users.Include(x=>x.Cars).FirstOrDefaultAsync(x=>x.Id== id);
            if(res==null)
                return null;
            _db.Cars.RemoveRange(res.Cars);
            _db.Users.Remove(res);
            await _db.SaveChangesAsync();
            return res;
        }
    }
}