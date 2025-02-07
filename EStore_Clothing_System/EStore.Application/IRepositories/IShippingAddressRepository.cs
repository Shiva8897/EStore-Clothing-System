using EStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.IRepositories
{
    public interface IShippingAddressRepository
    {   
            Task<IEnumerable<ShippingAddress>> GetAllAddressesAsync();
            Task<ShippingAddress> GetAddressByIdAsync(int shippingId);
            Task<IEnumerable<ShippingAddress>> GetAddressesByUserIdAsync(int userId);
            Task AddAddressAsync(ShippingAddress address);
            Task UpdateAddressAsync(ShippingAddress address);
            Task DeleteAddressAsync(int shippingId);        
    }
}
