using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.EntityDtos
{
    public class AddProductVariantDto
    {
        public string Size { get; set; }
        public string Color { get; set; }
        public decimal PricePerUnit { get; set; }
        public int Quantity { get; set; }
        
       
    }
}
