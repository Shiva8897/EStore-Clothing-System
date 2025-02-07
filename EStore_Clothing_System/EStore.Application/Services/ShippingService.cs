using AutoMapper;
using EStore.Application.Interfaces;
using EStore.Application.IRepositories;
using EStore.Domain.Entities;
using EStore.Domain.EntityDtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Services
{
    public class ShippingService : IShippingService
    {
        private readonly IShippingsRepository _shippingRepository;
       private readonly IMapper _mapper;
        public ShippingService(IShippingsRepository shippingRepository,IMapper mapper)
        {
          _shippingRepository= shippingRepository;
            _mapper= mapper;    
            
        }

        public async Task<ShippingDto> Createshipping(ShippingDto shippingDto)
        {
            var shipping= _mapper.Map<Shipping>(shippingDto);

             var shippingRes=await _shippingRepository.Createshipping(shipping);

            return _mapper.Map<ShippingDto>(shippingRes);
            
        }

        public async Task<ShippingDto> GetShippingByOrderId(int orderId)
        {
            var shipping=_shippingRepository.GetShippingDetailsById(orderId);

            return _mapper.Map<ShippingDto>(shipping);
        }
    }
}
