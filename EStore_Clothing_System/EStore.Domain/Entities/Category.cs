using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.Entities
{
     public class Category
     {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        // Navigation Property
       public virtual ICollection<SubCategory> SubCategories { get; set; }
         public virtual ICollection<Product> Products { get; set; }
    }
}
