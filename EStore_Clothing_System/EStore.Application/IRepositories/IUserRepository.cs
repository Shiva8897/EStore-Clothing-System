using EStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.IRepositories
{
    public interface IUserRepository
    {
        Task<User> RegisterUser(User user);
        Task<User> GetUserByEmail(string email);
        Task<User> UpdateUserPassword(User user);
    }
}
