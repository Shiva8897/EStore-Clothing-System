using AutoMapper;
using EStore.Application.Interfaces;
using EStore.Domain.Entities;
using EStore.Domain.EntityDtos.NewFolder;
using EStore.Domain.EntityDtos.OrderDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace EStore.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
            
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnOder([FromBody] OrderReq orderReq)
        {
            if (orderReq == null) return BadRequest("Order Cannot be null");
            try
            {        
                var orderResponse = await _orderService.CreateAnOrderAsync(orderReq);
                              
                return Ok(orderResponse);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("{orderId}")]
       public async Task<IActionResult> GetOrderById(int orderId)
        {

            if (orderId <= 0)
            {
                return BadRequest("Invalid order ID.");
            }

            try
            {
                var order = await _orderService.GetOrderByIdAsync(orderId);
                if (order == null)
                {
                    return NotFound($"Order with ID {orderId} not found.");
                }
                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
    
        [Route("user/{userId}")]
        public async Task<IActionResult> GetOrdersByUserId(int userId)
        {
            if (userId == null)
            {
                return BadRequest("userId cannot be null");
            }
            try
            {
               var orders= await _orderService.GetOrdersByUserIdAsync(userId);
                if(orders==null || !orders.Any())
                {
                    return NotFound("No Orders forun for the specific User ID.");
                }
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("Confirmation/{orderId}")]
        public async Task<IActionResult> ChangeStatusOfOrder(int orderId)
        {
            try
            {
                var orderResponse = await _orderService.ChangeStatusOfOrder(orderId);
                return Ok(orderResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut]
        [Route("cancel/{orderId}")]
        public async Task<IActionResult> CancelOrderByIdasync(int orderId)
        {
            try
            {
                var orderResponse = await _orderService.CancelOrderById(orderId);
                return Ok(orderResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet]
        [Route("{orderId}/total-amount")]
        public async Task<ActionResult<decimal>> GetTotalAmount(int orderId, [FromQuery] string couponCode = null)
        {
            try
            {
                var totalAmount = await _orderService.CalculateTotalAmountAsync(orderId, couponCode);

                // Return the updated total amount
                return Ok(totalAmount);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("delete/{orderId}")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            try
            {
                // Call the service to delete the order
                var result = await _orderService.DeleteOrderByIdAsync(orderId);

                if (result)
                {
                    
                    return Ok("Order deleted successfully.");
                }
                else
                {
                   
                    return NotFound("Order not found.");
                }
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
        [HttpPost("sendOrderCancelDetails")]
        public async Task<IActionResult> SendOrderCancelDetails(OrderEmailRequest request)
        {
            try
            {
                if (request == null || string.IsNullOrEmpty(request.Email))
                {
                    return BadRequest("Invalid request data.");
                }
                await _orderService.SendOrderCancelDetails(request);

                return Ok("Order Cancelled Successfully.");
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
        [HttpPost("sendOrderDetails")]
        public async Task<IActionResult> SendOrderDetails(OrderEmailRequest request)
        {
            try
            {
                if (request == null || string.IsNullOrEmpty(request.Email))
                {
                    return BadRequest("Invalid request data.");
                }

                await _orderService.SendOrderDetails(request);

                return Ok("Order details sent successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
      

    }
}
