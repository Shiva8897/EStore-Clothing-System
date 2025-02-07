using EStore.Application.IRepositories;
using EStore.Domain.Entities;
using EStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EStoreDbContext _eStoreDbContext;

        public OrderRepository(EStoreDbContext eStoreDbContext)
        {
            _eStoreDbContext = eStoreDbContext;
        }

        public async Task<bool> ApplyCouponAsync(int orderId, string couponCode)
        {
            var order=await GetOrderByIdAsync(orderId);

            if (order == null) 
                return false;

            var coupon=await _eStoreDbContext.Coupons
                        .FirstOrDefaultAsync(c=>c.CouponCode == couponCode);    

            if (coupon == null || !coupon.IsActive || coupon.ExpirationDate<DateTime.Now) 
                return false;

            //Applying Coupon Discount to the Order
            order.CouponId = coupon.CouponId;
            _eStoreDbContext.Orders.Update(order);
            await _eStoreDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<decimal> CalculateTotalAmountAsync(int orderId, string? couponCode)
        {
            var order = await GetOrderByIdAsync(orderId);

            if (order == null)
                throw new ArgumentException("Order was Not Found");

            decimal totalAmount = 0;
            totalAmount = order.OrderItems.Sum(o => o.Quantity * o.Price);

            //Applying Coupon 
            if (!string.IsNullOrWhiteSpace(couponCode))
            {
                var coupon = await _eStoreDbContext.Coupons
                                .FirstOrDefaultAsync(c => c.CouponCode == couponCode);

                if (coupon != null && coupon.IsActive && coupon.ExpirationDate >= DateTime.Now)
                {
                    // Apply the coupon to the order and update it
                    order.CouponId = coupon.CouponId;
                    if (totalAmount >= 999)
                    {
                        totalAmount -= coupon.DiscountedAmount;
                    }                 
                }
                else
                {
                    throw new ArgumentException("Invalid or Expired coupon.");
                }
            }

            order.TotalAmount = totalAmount;
            _eStoreDbContext.Orders.Update(order);
            await _eStoreDbContext.SaveChangesAsync();
            return totalAmount;
        }

        public async Task<Order> ChangeStatusOfOrder(int orderId)
        {
            /*  var order=await GetOrderByIdAsync(orderId);
               if(order == null)
               {
                   return null;
               }
               order.status = "Confirmed";
               _eStoreDbContext.Orders.Update(order);
               await _eStoreDbContext.SaveChangesAsync();
               return order;*/
            var order = await GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return null;
            }
            order.status = "Confirmed";
            foreach (var orderItem in order.OrderItems)
            {
                var productVariant = await _eStoreDbContext.ProductVariants
                    .FindAsync(orderItem.ProductVariantId);

                // Check if the product variant exists
                if (productVariant == null)
                {
                    throw new InvalidOperationException($"Product variant with ID {orderItem.ProductVariantId} does not exist.");
                }

                // Ensure there's enough quantity available before reducing
                if (productVariant.Quantity < orderItem.Quantity)
                {
                    throw new InvalidOperationException($"Not enough quantity for product variant {productVariant.Name}. Available: {productVariant.Quantity}, Requested: {orderItem.Quantity}");
                }

                // Deduct the ordered quantity from the product variant
                productVariant.Quantity -= orderItem.Quantity;
            }

            // Update the order in the context
            _eStoreDbContext.Orders.Update(order);

            // Save changes to the database
            await _eStoreDbContext.SaveChangesAsync();

            return order;
        }

        public async Task<Order> CreateAnOrderAsync(Order order)
        {
           
                _eStoreDbContext.Orders.Add(order);
                await _eStoreDbContext.SaveChangesAsync();
                return order;
            
           
        }

        public async Task<bool> DeleteOderByIdAsync(int orderId)
        {
            var order =await GetOrderByIdAsync(orderId);

            if (order == null) return false;

            _eStoreDbContext.Orders.Remove(order);
            await _eStoreDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            var order=await _eStoreDbContext.Orders
                            .Include(o=>o.User)
                            .Include(o=>o.OrderItems)
                            .FirstOrDefaultAsync(o=>o.Id==orderId);
            return order;
        }


        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
        {
            var userOrders = await _eStoreDbContext.Orders
                            .Where(o => o.UserId == userId)
                          
                            .Include(o => o.OrderItems)
                             .Include(o=>o.Shipping)
                            .Include(o=>o.User)
                            .ToListAsync();
            return userOrders;
        }

        public async Task<bool> ProcessPayment(int orderId)
        {
            var order = await GetOrderByIdAsync(orderId);
            if(order == null)
               return false;

            var payment = new Payment
            {
                OrderId= orderId,
                Amount=order.TotalAmount,
                Date=DateTime.Now,
                PaymentMode="Credit Card",
                PaymentStatus="Success"
            };

            _eStoreDbContext.Payment.Add(payment);
            await _eStoreDbContext.SaveChangesAsync();
            return true;

        }

      /*  public async Task<Order> RemoveOrderItemAsync(int orderItemId)
        {
            var orderItem = await GetOrderItemByIdAsync(orderItemId);

            if (orderItem == null)
                return null;

            var order = await GetOrderByIdAsync(orderItem.OrderId);
            if (order == null)
                return null;

            _eStoreDbContext.OrderItems.Remove(orderItem);
            await _eStoreDbContext.SaveChangesAsync();

            order.OrderItems.Remove(orderItem);
            await UpdateOrderasync(order);

            return order;
        }*/

       /* public async Task<OrderItem> GetOrderItemByIdAsync(int orderItemId)
        {
            return await _eStoreDbContext.OrderItems.FindAsync(orderItemId);
        }
*/

        public async Task UpdateOrderasync(Order order)
        {
            _eStoreDbContext.Orders.Update(order);
            await _eStoreDbContext.SaveChangesAsync();
        }

        public async Task<Dictionary<int, decimal>> GetProductVariantPricesAsync(List<int> productVariantIds)
        {
            // Assuming _dbContext is your Entity Framework context
            var prices = await _eStoreDbContext.ProductVariants
                                         .Where(pv => productVariantIds.Contains(pv.ProductVariantId))
                                         .ToDictionaryAsync(pv => pv.ProductVariantId, pv => pv.PricePerUnit); // Replace 'Price' with the actual field name
            return prices;
        }

        public async Task<Order> CancelOrderById(int orderId)
        {
            var order = await _eStoreDbContext.Orders
                         .Include(o => o.Shipping) // Include the Shipping entity
                         .FirstOrDefaultAsync(o => o.Id == orderId);
            if (order.Shipping.ShippigDate < DateTime.Now)
            {
                return order;
            }
            foreach (var orderItem in order.OrderItems)
            {
                var productVariant = await _eStoreDbContext.ProductVariants
                                      .FindAsync(orderItem.ProductVariantId);

                // Check if the product variant exists
                if (productVariant == null)
                {
                    throw new InvalidOperationException($"Product variant with ID {orderItem.ProductVariantId} does not exist.");
                }

                // Add back the ordered quantity to the product variant's available quantity
                productVariant.Quantity += orderItem.Quantity;
            }

            order.status = "Cancelled";
            _eStoreDbContext.Orders.Update(order);
            await _eStoreDbContext.SaveChangesAsync();
            return order;
        }
    }
}
