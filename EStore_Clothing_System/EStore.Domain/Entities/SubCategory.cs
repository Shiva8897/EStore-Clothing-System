using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.Entities
{
    public class SubCategory
    {
        public int SubCategoryId { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        //Navigation Properties
       public virtual Category Category { get; set; }
       public virtual ICollection<Product> Products { get; set; }

    }
}
