using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.EntityDtos
{
    public class ShippingAddressRequest
    {
        public int ShippingAddressId { get; set; }
        public int UserId { get; set; }
        public string Street { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
    }
}
