using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using carwash.Models.Domain;
using carwash.Models.DTO;

namespace carwash.Repositories
{
    public interface IUser
    {
        Task<List<User>> GetAllUserAsync();
        Task<User> GetUserByIdAsync(Guid userId);
        Task<User> AddUserAsync(User obj);
        Task<User> UpdateUserAsync(Guid id, User obj);
        Task<User> DeleteUserAsync(Guid id);
    }
}