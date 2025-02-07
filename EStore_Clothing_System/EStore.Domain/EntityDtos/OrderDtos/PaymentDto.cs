using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.EntityDtos.OrderDtos
{
    public class PaymentDto
    {
      
        public decimal Amount { get; set; }

        public string PaymentMode { get; set; }
    }
}
