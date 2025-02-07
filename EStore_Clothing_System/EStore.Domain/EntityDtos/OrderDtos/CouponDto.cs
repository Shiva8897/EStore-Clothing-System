using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.EntityDtos.OrderDtos
{
    public class CouponDto
    {
      public int CouponId { get; set; }
        public string CouponCode { get; set; }
        public decimal DiscountedAmount { get; set; }
    }
}
