using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.EntityDtos
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDesrciption { get; set; }
        public string Brand { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public decimal PricePerUnit { get; set; }
        public string ImageData { get; set; }
        public string ImageBase64 { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public double Price {  get; set; }
        public int Quantity { get; set; }
        public ICollection<ProductVariantDto> ProductVariants { get; set; }
    }

   
}
