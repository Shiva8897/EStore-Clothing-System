using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.EntityDtos.OrderDtos
{
    public class ShippingDto
    {
        public int ShippingId { get; set; }

        public int OrderId { get; set; }
         
        public string TrackingNumber { get; set; }

        public DateTime ShippigDate { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
    }
}
