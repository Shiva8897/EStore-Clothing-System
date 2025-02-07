using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int? CouponId { get; set; }
        public int OrderQuantity {  get; set; }
        public string status { get; set; }

        // Navigation Properties in order
        public virtual User User { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; } //= null;
        public virtual Coupon Coupon { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual Shipping Shipping { get; set; }
    }
}
