using EStore.Application.Interfaces;
using EStore.Domain.EntityDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EStore.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;
        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddToWishlist([FromBody] WishlistReq request)
        {                                  
            var wishlistItem = await _wishlistService.AddToWishlistAsync(request.UserId, request.ProductId);
            return Ok(wishlistItem);
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveFromWishlist([FromBody] WishlistReq request)
        {
            var result = await _wishlistService.RemoveFromWishlistAsync(request.UserId, request.ProductId);
            if (result)
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetWishlist(int userId)
        {
            var wishlist = await _wishlistService.GetWishlistByUserIdAsync(userId);
            return Ok(wishlist);
        }
    }
}
