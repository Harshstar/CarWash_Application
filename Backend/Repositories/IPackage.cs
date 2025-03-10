using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using carwash.Models.Domain;

namespace carwash.Repositories
{
    public interface IPackage
    {
        Task<IEnumerable<Package>> GetAllPackagesAsync();
        Task<Package> GetPackageByIdAsync(Guid id);
        Task<Package> CreatePackageAsync(Package obj);
        Task <Package> UpdatePackageAsync(Guid id , Package obj);
        Task <Package> DeletePackageAsync(Guid id);
    }
}