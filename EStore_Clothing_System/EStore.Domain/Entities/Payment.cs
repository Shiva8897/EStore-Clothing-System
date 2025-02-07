using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.Entities
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public string PaymentMode { get; set; }

        public string PaymentStatus { get; set; }

       public int OrderId { get; set; }

       public virtual Order Order { get; set; }
    }
}
