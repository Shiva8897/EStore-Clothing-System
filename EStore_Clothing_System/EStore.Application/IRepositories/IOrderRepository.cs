using EStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.IRepositories
{
    public interface IOrderRepository
    {
        //create an Order
        Task<Order> CreateAnOrderAsync(Order order);

        //Getting and order by its Id
        Task<Order> GetOrderByIdAsync(int orderId);

        //Getting all Orders of a Specific User
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);

        //Cancelling and Order
        Task<Order> ChangeStatusOfOrder(int orderId);

        //Applying a Coupon
        Task<bool> ApplyCouponAsync(int orderId, string couponCode);

        //Calculating the Total amount of Order
        Task<decimal> CalculateTotalAmountAsync(int orderId,string? couponCode);

        //IsPayment Success or not for Order
        Task<bool> ProcessPayment(int orderId);

        Task<Dictionary<int, decimal>> GetProductVariantPricesAsync(List<int> productVariantIds);

       // Task<Order> RemoveOrderItemAsync(int orderItemId);
       // Task<OrderItem> GetOrderItemByIdAsync(int orderItemid);
        Task UpdateOrderasync(Order order);

        Task<bool> DeleteOderByIdAsync(int orderId);
        Task<Order> CancelOrderById(int orderId);

    }
}
