using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.Entities
{
    public class ProductVariant
    {
        public int ProductVariantId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public decimal PricePerUnit {  get; set; }
        public int Quantity { get; set; }
       

        // Navigation Property
        public virtual Product Product { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<ProductReview> ProductReviews { get; set; }
   
    }
}
