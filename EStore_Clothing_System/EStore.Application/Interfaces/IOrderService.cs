using EStore.Domain.Entities;
using EStore.Domain.EntityDtos.NewFolder;
using EStore.Domain.EntityDtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Interfaces
{
    public interface IOrderService
    {
        //create an Order
        Task<OrderRes> CreateAnOrderAsync(OrderReq orderReq);

        //Getting and order by its Id
        Task<OrderRes> GetOrderByIdAsync(int orderId);

        //Getting all Orders of a Specific User
        Task<IEnumerable<OrderRes>> GetOrdersByUserIdAsync(int userId);

        //Cancelling and Order
        Task<OrderRes> ChangeStatusOfOrder(int orderId);

       // Task<OrderRes> RemoveOrderItemAsync(int orderItemId);

        //Applying a Coupon
        Task<bool> ApplyCouponAsync(int orderId, string couponCode);

        //Calculating the Total amount of Order
        Task<decimal> CalculateTotalAmountAsync(int orderId, string? couponCode);

        //IsPayment Success or not for Order
        Task<bool> ProcessPayment(int orderId);
        Task<bool> DeleteOrderByIdAsync(int orderId);
        Task<OrderRes> CancelOrderById(int orderId);

        Task SendOrderCancelDetails(OrderEmailRequest request);
        Task SendOrderDetails(OrderEmailRequest request);



    }
}
