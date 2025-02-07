using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.Entities
{
     public  class Cart
    {
        public int CartId { get; set; }
        //public int ProductVariantId {  get; set; }  
        public int Quantity { get; set; }
        public int UserId {  get; set; }
      //  public User User { get; set; }
       // public ProductVariant ProductVariant { get; set; }
    }
}
