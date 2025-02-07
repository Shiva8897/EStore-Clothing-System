using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.EntityDtos.NewFolder
{
    public class OrderItemRes
    {
       
        public int ProductVariantId { get; set; }
        public int Quantity { get; set; }
       public decimal Price { get; set; }
    }
}
