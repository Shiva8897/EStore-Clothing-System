using AutoMapper;
using EStore.Application.Interfaces;
using EStore.Application.IRepositories;
using EStore.Domain.Entities;
using EStore.Domain.EntityDtos.NewFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Services
{
    public class OrderItemservice : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        public OrderItemservice(IOrderItemRepository orderIteRepository,IMapper mapper, IOrderRepository orderRepository)
        {
            _orderItemRepository = orderIteRepository;
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        public async Task<OrderRes> AddOrderItemAsync(int orderId, OrderItemreq ordertemReq)
        {
           var order=await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null) return null;

         
            var orderItem=_mapper.Map<OrderItem>(ordertemReq);
            orderItem.OrderId = orderId;
            order.OrderItems.Add(orderItem);
            await _orderItemRepository.AddOrderItemAsync(orderItem);
           await _orderRepository.UpdateOrderasync(order);

            return _mapper.Map<OrderRes>(order);

        }

        public async Task<OrderItem> GetOrderItemByIdAsync(int orderItemid)
        {
            var orderItem = await _orderItemRepository.GetOrderItemByIdAsync(orderItemid);
            return orderItem;
        }

        public async Task<OrderRes> RemoveOrderItemAsync(int orderItemId)
        {
            if (orderItemId <= 0)
                throw new ArgumentException("Invalid order ID.");

            var orderItem = await GetOrderItemByIdAsync(orderItemId);
            var order = await _orderRepository.GetOrderByIdAsync(orderItem.OrderId);
            if (order == null)
                throw new KeyNotFoundException($"Order with ID {orderItem.OrderId} not found");

            await _orderItemRepository.RemoveOrderItemAsync(orderItemId);
            await _orderRepository.UpdateOrderasync(order);

            var updatedOrder = await _orderRepository.GetOrderByIdAsync(order.Id);

            return _mapper.Map<OrderRes>(updatedOrder);
        }

        public async Task<OrderItemRes> UpdateOrderItemAsync(int orderItemId, OrderItemreq orderItemReq)
        {
            var existingOrderItem = await _orderItemRepository.GetOrderItemByIdAsync(orderItemId);

            if (existingOrderItem == null)
                return null;

            existingOrderItem.ProductVariantId = orderItemReq.ProductVariantId;
            existingOrderItem.Quantity = orderItemReq.Quantity;

          
          var updatedorderItemRes=  await _orderItemRepository.UpdateOrderItemAsync(orderItemId, existingOrderItem);

            return _mapper.Map<OrderItemRes>(updatedorderItemRes);
        }
    }
}
