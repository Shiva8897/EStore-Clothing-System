using EStore.Domain.Entities;
using EStore.Domain.EntityDtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Interfaces
{
    public interface IShippingService
    {
        Task<ShippingDto> Createshipping(ShippingDto shippingDto);
        Task<ShippingDto>  GetShippingByOrderId(int orderId);
    }
}
