using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.Entities
{
    public class Coupon
    {
        public int CouponId { get; set; }

        public string CouponCode { get; set; }
        public decimal DiscountedAmount { get; set; }

        public bool IsActive { get; set; }

        public bool IsApplicable { get; set; }

        public DateTime ExpirationDate { get; set; }

        public virtual Order Orders { get; set; } 
    }
}
