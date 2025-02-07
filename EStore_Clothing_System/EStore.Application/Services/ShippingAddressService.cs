using AutoMapper;
using EStore.Application.Interfaces;
using EStore.Application.IRepositories;
using EStore.Domain.Entities;
using EStore.Domain.EntityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Services
{
    public class ShippingAddressService : IShippingAddressService
    {
        private readonly IShippingAddressRepository _repository;
        private readonly IMapper _mapper;

        public ShippingAddressService(IShippingAddressRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ShippingAddressResponse>> GetAllAddressesAsync()
        {
            var addresses = await _repository.GetAllAddressesAsync();
            return _mapper.Map<IEnumerable<ShippingAddressResponse>>(addresses);
        }

        public async Task<ShippingAddressResponse> GetAddressByIdAsync(int id)
        {
            var address = await _repository.GetAddressByIdAsync(id);
            return _mapper.Map<ShippingAddressResponse>(address);
        }

        public async Task<IEnumerable<ShippingAddressResponse>> GetAddressesByUserIdAsync(int userId)
        {
            var addresses = await _repository.GetAddressesByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<ShippingAddressResponse>>(addresses);
        }

        public async Task AddAddressAsync(ShippingAddressRequest addressRequest)
        {
            var address = _mapper.Map<ShippingAddress>(addressRequest);
            await _repository.AddAddressAsync(address);
        }

        public async Task UpdateAddressAsync(int shippingId, ShippingAddressRequest addressRequest)
        {
            var address = _mapper.Map<ShippingAddress>(addressRequest);
            address.ShippingAddressId = shippingId; 
            await _repository.UpdateAddressAsync(address);
        }

        public async Task DeleteAddressAsync(int shippingId)
        {
            await _repository.DeleteAddressAsync(shippingId);
        }
    }
}
