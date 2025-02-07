using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.Entities
{
    public class ProductReview
    {
        public int ProductReviewId { get; set; }
        public int UserId { get; set; }
        public int ProductVariantId { get; set; }
        public int Rating { get; set; }
        public string ReviewText { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual User User { get; set; }
        public virtual ProductVariant Productvariants { get; set; }

    }
}
