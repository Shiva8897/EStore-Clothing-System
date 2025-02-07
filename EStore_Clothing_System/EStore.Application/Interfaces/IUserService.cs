using EStore.Domain.Entities;
using EStore.Domain.EntityDtos;
using EStore.Domain.EntityDtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Interfaces
{
     public interface IUserService
     {
        Task<User> RegisterUser(UserReq user);
        Task<User> GetUserByEmail(string email);
        Task<User> UpdateUserPassword(User user);
        Task ShareOrderDetailsViaEmail(EmailReq emailReq);
    }
} 
