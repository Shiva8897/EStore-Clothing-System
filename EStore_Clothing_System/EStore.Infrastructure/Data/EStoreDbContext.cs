using EStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Infrastructure.Data
{
    public class EStoreDbContext : DbContext
    {
        public EStoreDbContext(DbContextOptions<EStoreDbContext> options) : base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<Refunds> Refunds { get; set; }
        public DbSet<Shipping> Shippings { get; set; }
        public DbSet<ShippingAddress> ShippingAddresses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WishList> WishList { get; set; }
        public DbSet<Cart> Carts { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<SubCategory>()
              .HasOne(p => p.Category)
              .WithMany(c => c.SubCategories)
              .HasForeignKey(p => p.CategoryId);

           modelBuilder.Entity<Product>()
                  .HasOne(p => p.SubCategory)
                  .WithMany(sc => sc.Products)
                  .HasForeignKey(p => p.SubCategoryId);
            //Order and Coupon
            modelBuilder.Entity<Order>()
               .HasOne(o => o.Coupon)
               .WithOne(c => c.Orders)
               .HasForeignKey<Order>(o => o.CouponId);
            //Order and User 
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);
            //OrderItem and Order 
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);
            //orderItem Product variant
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Productvariants)
                .WithMany(pv=>pv.OrderItems)
                .HasForeignKey(oi=>oi.ProductVariantId);
            //payment and order
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Order)
                .WithOne(o=>o.Payment)
                .HasForeignKey<Payment>(p => p.OrderId);

            modelBuilder.Entity<ProductVariant>()
                 .HasOne(pv => pv.Product)
                 .WithMany(p => p.ProductVariants)
                 .HasForeignKey(pv => pv.ProductId);
            //ProductReview and User
            modelBuilder.Entity<ProductReview>()
                .HasOne(pr => pr.User)
                .WithMany(u => u.ProductReviews)
                .HasForeignKey(pr => pr.UserId);

            //ProductReviews and Product 
            modelBuilder.Entity<ProductReview>()
                .HasOne(pr => pr.Productvariants)
                .WithMany(pv => pv.ProductReviews)
                .HasForeignKey(pr => pr.ProductVariantId);
            //Refund and OrderItem 
            modelBuilder.Entity<Refunds>()
                .HasOne(r => r.OrderItem)
                .WithOne(oi=>oi.Refunds)
                .HasForeignKey<Refunds>(r => r.OrderItemId);

            //shipping and Order
            modelBuilder.Entity<Shipping>()
                .HasOne(s => s.Order)
                .WithOne(o=>o.Shipping)
                .HasForeignKey<Shipping>(s => s.OrderId);
            //Adress and User 
            modelBuilder.Entity<ShippingAddress>()
                .HasOne(sa => sa.User)
                .WithMany(u => u.ShippingAddresses)
                .HasForeignKey(sa => sa.UserId);
            //WishList and Product
            modelBuilder.Entity<WishList>()
            .HasMany(w => w.Product)
            .WithOne(); 

            //WishList and User 
            modelBuilder.Entity<WishList>()
                .HasOne(w => w.User)
                .WithOne(u => u.WishList)
                .HasForeignKey<WishList>(w=>w.UserId);     
        }
    } 
}
