using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
         public string LongDesrciption { get; set; }
        public string Brand { get; set; }     
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public byte[] ImageData { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        //navigation in product
        public Category Category { get; set; }
        public SubCategory SubCategory { get; set; }
        //this is product detail navigation
        public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
      
       
    }
}
