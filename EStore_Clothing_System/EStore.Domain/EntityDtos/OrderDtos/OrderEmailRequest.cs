using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.EntityDtos.OrderDtos
{
    public class OrderEmailRequest
    {
        public string Email { get; set; }
        public int OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderDate { get; set; }
    }
}
