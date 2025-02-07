using EStore.Domain.Entities;
using EStore.Domain.EntityDtos.NewFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Interfaces
{
    public interface IOrderItemService
    {
        Task<OrderItem> GetOrderItemByIdAsync(int orderItemid);

        //Remove an Order item from the Orders
        Task<OrderRes> RemoveOrderItemAsync(int orderItemId);

        Task<OrderRes> AddOrderItemAsync(int orderId, OrderItemreq ordertemReq);
        Task<OrderItemRes> UpdateOrderItemAsync(int orderItemId, OrderItemreq orderItemReq);

    }
}
