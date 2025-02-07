using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.Entities
{
    public class User
    {

        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "Password cannot exceed 100 characters")]
        public string PasswordHash { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
        //navigation Properties 
        public string Role {  get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual WishList WishList { get; set; }

       public virtual ICollection<ProductReview> ProductReviews { get; set; }

        public virtual ICollection<ShippingAddress> ShippingAddresses { get; set; }

    }
}
