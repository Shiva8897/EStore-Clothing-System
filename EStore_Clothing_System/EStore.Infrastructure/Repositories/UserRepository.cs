using EStore.Application.IRepositories;
using EStore.Domain.Entities;
using EStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private EStoreDbContext _context;
        public UserRepository(EStoreDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            
            var result= await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (result == null)
            {
                return null;
            }
            return result;  
        }
        public async Task<User> RegisterUser(User user)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(c => c.Email == user.Email);
            if (existingUser != null)
            {
                return null;
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<User> UpdateUserPassword(User user)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(c => c.UserId == user.UserId);
            if (existingUser == null)
            {
                return null;
            }
            existingUser.PasswordHash = user.PasswordHash;
            await _context.SaveChangesAsync();
            return user;
        }
    }
} 
