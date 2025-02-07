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
    public class ShippingAddressRepository : IShippingAddressRepository
    {
        private readonly EStoreDbContext _dbContext;

        public ShippingAddressRepository(EStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ShippingAddress>> GetAllAddressesAsync()
        {
            return await _dbContext.ShippingAddresses
                .AsNoTracking() 
                .Include(sa => sa.User)
                .ToListAsync();
        }


        public async Task<ShippingAddress> GetAddressByIdAsync(int shippingId)
        {
            if(shippingId <= 0)
                throw new ArgumentOutOfRangeException(nameof(shippingId));
            return await _dbContext.ShippingAddresses.Include(sa => sa.User)
                                                   .FirstOrDefaultAsync(sa => sa.ShippingAddressId == shippingId);
        }

        public async Task<IEnumerable<ShippingAddress>> GetAddressesByUserIdAsync(int userId)
        {
            return await _dbContext.ShippingAddresses.Include(sa => sa.User)
                                                   .Where(sa => sa.UserId == userId)
                                                   .ToListAsync();
        }

        public async Task AddAddressAsync(ShippingAddress address)
        {
            _dbContext.ShippingAddresses.Add(address);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAddressAsync(ShippingAddress address)
        {
            _dbContext.ShippingAddresses.Update(address);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAddressAsync(int shippingId)
        {
            var address = await _dbContext.ShippingAddresses.FindAsync(shippingId);
            if (address != null)
            {
                _dbContext.ShippingAddresses.Remove(address);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
