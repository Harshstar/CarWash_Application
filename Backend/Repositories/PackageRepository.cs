using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using carwash.Data;
using carwash.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace carwash.Repositories
{
    public class PackageRepository : IPackage
    {
        private readonly CarwashDbContext _db;
        public PackageRepository(CarwashDbContext db)
        {
            _db=db;
        }
        public async Task<IEnumerable<Package>> GetAllPackagesAsync()
        {
            var res = await _db.Packages.ToListAsync();
            return res;
        }
        public async Task<Package> GetPackageByIdAsync(Guid id)
        {
            var res= await _db.Packages.FirstOrDefaultAsync(x => x.Id == id);
            return res;
        }
        public async Task<Package> CreatePackageAsync(Package obj)
        {
            await _db.Packages.AddAsync(obj);
            await _db.SaveChangesAsync();
            return obj;
        }

        public async Task<Package> UpdatePackageAsync(Guid id , Package obj)
        {
            var res = await _db.Packages.FirstOrDefaultAsync(x=>x.Id == id);
            if(res==null)
                return null;
            res.PackName = obj.PackName;
            res.PackPrice = obj.PackPrice;
            res.Duration = obj.Duration;
            res.Description = obj.Description;
            await _db.SaveChangesAsync();
            return res;
        }
        public async Task<Package> DeletePackageAsync(Guid id)
        {
            var res = await _db.Packages.FirstOrDefaultAsync(x=>x.Id== id);
            if(res==null)
                return null;
            _db.Packages.Remove(res);
            await _db.SaveChangesAsync();
            return res;
        }

    }
}