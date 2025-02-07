using EStore.Application.Interfaces;
using EStore.Domain.Entities;
using EStore.Domain.EntityDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EStore.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingAddressController : ControllerBase
    {
        private readonly IShippingAddressService _service;

        public ShippingAddressController(IShippingAddressService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShippingAddressResponse>>> GetAllAddresses()
        {
            try
            {
                var addresses = await _service.GetAllAddressesAsync();
                return Ok(addresses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpGet("{shippingId}")]
        public async Task<ActionResult<ShippingAddressResponse>> GetAddress(int shippingId)
        {
            try
            {
                var address = await _service.GetAddressByIdAsync(shippingId);
                if (address == null)
                {
                    return NotFound();
                }
                return Ok(address);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<ShippingAddressResponse>>> GetAddressesByUserId(int userId)
        {
            try
            {
                var addresses = await _service.GetAddressesByUserIdAsync(userId);
                return Ok(addresses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }
        [Authorize]
        [HttpPost]
 
        public async Task<ActionResult> CreateAddress([FromBody] ShippingAddressRequest addressRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _service.AddAddressAsync(addressRequest);
                return CreatedAtAction(nameof(GetAddress), new { shippingId = addressRequest.ShippingAddressId }, addressRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAddress(int id, [FromBody] ShippingAddressRequest addressRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != addressRequest.ShippingAddressId)
            {
                return BadRequest("The address ID in the route does not match the ID in the request body.");
            }

            try
            {
                await _service.UpdateAddressAsync(id, addressRequest);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Address not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAddress(int id)
        {
            try
            {
                await _service.DeleteAddressAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Address not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

    }
}
