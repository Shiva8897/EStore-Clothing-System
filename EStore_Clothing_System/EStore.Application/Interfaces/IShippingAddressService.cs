using EStore.Domain.Entities;
using EStore.Domain.EntityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Interfaces
{
    public interface IShippingAddressService
    {
        Task<IEnumerable<ShippingAddressResponse>> GetAllAddressesAsync();
        Task<ShippingAddressResponse> GetAddressByIdAsync(int shippingId);
        Task<IEnumerable<ShippingAddressResponse>> GetAddressesByUserIdAsync(int userId);
        Task AddAddressAsync(ShippingAddressRequest addressRequest);
        Task UpdateAddressAsync(int shippingId, ShippingAddressRequest addressRequest);
        Task DeleteAddressAsync(int shippingId);
    }
}
