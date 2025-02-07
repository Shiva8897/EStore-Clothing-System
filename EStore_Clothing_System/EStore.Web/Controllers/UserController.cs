using EStore.Application.Interfaces;
using EStore.Domain.Entities;
using EStore.Domain.EntityDtos;
using EStore.Domain.EntityDtos.OrderDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EStore.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet()]
        [Route("email/{email}")]
        public async Task<ActionResult<User>> GetUserByEmailAsync(string email)
        {
            try
            {               
                var result = await _userService.GetUserByEmail(email);
                if(result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("RegisterUser")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser([FromBody] UserReq userReq)
        {
            //validation of model 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Returns 400 
            }

            try
            {
                var newUser = await _userService.RegisterUser(userReq);

                if (newUser == null)
                {
                   //it will be null if usr already exists
                    return Conflict(new { message = "A user with this email already exists." });//returns 409
                }

                // Return the created user details with 201 Created status
                return CreatedAtAction(nameof(RegisterUser), new { id = newUser.UserId }, newUser);
            }
            catch (Exception ex)
            {             
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("send-email")]
        public IActionResult SendEmail([FromBody] EmailReq emailRequest)
        {
            if (string.IsNullOrEmpty(emailRequest.ToEmail) ||
                string.IsNullOrEmpty(emailRequest.Subject) ||
                string.IsNullOrEmpty(emailRequest.Body))
            {
                return BadRequest("Email details are required.");
            }

            _userService.ShareOrderDetailsViaEmail(emailRequest);
            return Ok("Email sent successfully.");
        }
    }
}
