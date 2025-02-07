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
    public class ShippingRepository : IShippingsRepository
    {
        private readonly EStoreDbContext _eStoreDbContext;
        private readonly Random random = new Random();
        public ShippingRepository(EStoreDbContext eStoreDbContext)
        {
            _eStoreDbContext = eStoreDbContext;
        }
        public async Task<Shipping> Createshipping(Shipping shipping)
        {
            
            shipping.TrackingNumber = random.Next(100000,999999).ToString();
            shipping.ShippigDate = DateTime.Now.AddDays(2);
            shipping.EstimatedDeliveryDate = DateTime.Now.AddDays(4);
        
            _eStoreDbContext.Add(shipping);
           await _eStoreDbContext.SaveChangesAsync();
            return shipping;
        }

        public async Task<IEnumerable<Shipping>> GetShippingDetailsById(int orderId)
        {
            var Shippings = await _eStoreDbContext.Shippings
                             .Where(o => o.OrderId == orderId).ToListAsync();

            return Shippings;
        }

    }
}
