using EStore.Application.IRepositories;
using EStore.Domain.Entities;
using EStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Infrastructure.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private EStoreDbContext _context;
     
        public LoginRepository(EStoreDbContext context)
        {
            _context = context;
            
        }
        public async Task<User> GetUserByCredentails(string email,string password)
        {
            var user = await _context.Users
            .SingleOrDefaultAsync(u => u.Email == email && u.PasswordHash == password);
            return user;
        }
    }
}
