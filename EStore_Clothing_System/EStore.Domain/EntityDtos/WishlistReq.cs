using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.EntityDtos
{
    public class WishlistReq
    {
        public int UserId {  get; set; }
        public int ProductId {  get; set; }
    }
}
