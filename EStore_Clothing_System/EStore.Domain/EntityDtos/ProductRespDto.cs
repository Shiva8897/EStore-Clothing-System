using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.EntityDtos
{
     public class ProductRespDto
     {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string ImageData { get; set; }
    }
}
