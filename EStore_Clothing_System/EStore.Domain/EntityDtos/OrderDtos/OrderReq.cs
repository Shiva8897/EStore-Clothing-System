using EStore.Domain.Entities;
using EStore.Domain.EntityDtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.EntityDtos.NewFolder
{
    public class OrderReq
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } 
        public int UserId { get; set; }
        public string status { get; set; }
        // public int? CouponId { get; set; }
        public ICollection<OrderItemreq> OrderItemreq { get; set; }
       //  public UserDto User { get; set; }
      //  public CouponDto Coupon { get; set; }
       // public PaymentDto Payment { get; set; }
       // public ShippingDto Shipping { get; set; }


    }
}
