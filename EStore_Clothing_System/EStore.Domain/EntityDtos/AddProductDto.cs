using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.EntityDtos
{
    public class AddProductDto
    {
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDesrciption { get; set; }
        public string Brand { get; set; }
        public IFormFile? ImageFile { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        [Required]
        public ICollection<AddProductVariantDto> addProductVariantDtos { get; set; }= new List<AddProductVariantDto>();

    }
}
