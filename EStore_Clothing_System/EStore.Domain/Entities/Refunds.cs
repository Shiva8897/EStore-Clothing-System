using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.Entities
{
    public class Refunds
    {

        public int RefundsId { get; set; }

        public int OrderItemId { get; set; }

        public decimal AmountRefunded { get; set; }

        public string Reason { get; set; }
        public DateTime CreatedDate { get; set; }

        public bool IsSuccess { get; set; }

        public virtual OrderItem OrderItem { get; set; }
    }
}
