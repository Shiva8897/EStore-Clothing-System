using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.EntityDtos
{
    public class ProductVariantDto
    {
        public int ProductVariantId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public decimal PricePerUnit { get; set; }
        public int Quantity { get; set; }
    }
}
