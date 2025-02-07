using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.Entities
{
    public class ShippingAddress
    {
        public int ShippingAddressId { get; set; }
        public int UserId { get; set; }
        public string Street { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
       public virtual User User { get; set; }

    }
}
