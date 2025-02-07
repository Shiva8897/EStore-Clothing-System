using EStore.Domain.Entities;
using EStore.Domain.EntityDtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.IRepositories
{
    public interface IShippingsRepository
    {
        Task<Shipping> Createshipping(Shipping shipping);
        Task<IEnumerable<Shipping>> GetShippingDetailsById(int orderId);
    }
}
