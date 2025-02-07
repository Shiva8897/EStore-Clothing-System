using EStore.Application.Interfaces;
using EStore.Application.Services;
using EStore.Domain.EntityDtos.NewFolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EStore.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;

        }
        [HttpDelete]
        [Route("RemoveOrderItem/{orderItemid}")]
        [Authorize]
        public async Task<IActionResult> RemoveItemFromOrder(int orderItemid)
        {
            try
            {
               
                var updateOrder = await _orderItemService.RemoveOrderItemAsync(orderItemid);
               
                    if (updateOrder == null)
                    {
                        return NotFound("No Orders found for the specific order ID.");
                    }
                return Ok(updateOrder);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("{orderrItemId}")]
        public async Task<IActionResult> GetOrderItemById(int orderrItemId)
        {
            try
            {
                var orderItem = await _orderItemService.GetOrderItemByIdAsync(orderrItemId);
                if(orderItem == null)
                {
                    return NotFound("No OrderItem found for the specific OrderItem ID.");
                }
                return Ok(orderItem);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("AddOrderItem/{orderId}")]
        [Authorize]
        public async Task<IActionResult> AddOrderItemToOrder(int orderId, [FromBody] OrderItemreq orderItemReq)
        {
            if (orderItemReq == null)
            {
                return BadRequest("Order item request cannot be null.");
            }

            try
            {
                var updatedOrder = await _orderItemService.AddOrderItemAsync(orderId, orderItemReq);

                if (updatedOrder == null)
                {
                    return NotFound("Order not found or order item could not be added.");
                }

                return Ok(updatedOrder);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateOrderItem/{orderItemId}")]
        [Authorize]
        public async Task<IActionResult> UpdateOrderItem(int orderItemId, [FromBody] OrderItemreq orderItemreq)
        {
            if (orderItemreq == null)
                return BadRequest("Order item request cannot be null.");

            try
            {
                var updatedOrderItem = await _orderItemService.UpdateOrderItemAsync(orderItemId, orderItemreq);

                if (updatedOrderItem == null)
                    return NotFound("Order item not found.");

                return Ok(updatedOrderItem);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
