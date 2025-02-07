using EStore.Application.Interfaces;
using EStore.Application.Services;
using EStore.Domain.Entities;
using EStore.Domain.EntityDtos;
using EStore.Domain.EntityDtos.NewFolder;
using EStore.Domain.EntityDtos.OrderDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EStore.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IPasswordRecoveryService _passwordRecoveryService;
        public LoginController(ILoginService loginService, IPasswordRecoveryService passwordRecoveryService)
        {
            _loginService = loginService;
            _passwordRecoveryService = passwordRecoveryService;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginReq login)
        {
            var loginRes = await _loginService.ProvideToken(login);


            if (loginRes==null)
            {
              // if loginRes is empty it returns 401 
                return Unauthorized(new { message = "Invalid email or password." });
            }

            return Ok(new 
            { token= loginRes.Token,
              role= loginRes.Role
            });
        }
        [HttpPost("send-reset-link")]
        public async Task<IActionResult> SendResetLink([FromBody] string email)
        {
            var result = await _passwordRecoveryService.SendResetLinkAsync(email);
            if (result)
            {
                return Ok(new { message = "Password reset link sent successfully." });
            }

            return BadRequest(new { message = "User not found or error sending email." });
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] PasswordResetReq request)
        {
            var result = await _passwordRecoveryService.ResetPasswordAsync(request.Email, request.Token, request.NewPassword);
            if (result)
            {
                return Ok(new { message = "Password has been reset successfully." });
            }

            return BadRequest(new { message = "Invalid token or email." });
        }

        [HttpPost("sendOrderDetails")]
        public async Task<IActionResult> SendOrderDetails(string Email, [FromBody] OrderReq order)
        {
            if (order == null || string.IsNullOrEmpty(Email))
            {
                return BadRequest("Invalid order details or email.");
            }

            try
            {
                // Send the order details via email
     
                return Ok("Order details sent successfully.");
            }
            catch (Exception ex)
            {
                // Handle exceptions such as email sending failures
                return StatusCode(500, $"Error sending email: {ex.Message}");
            }
        }
    }
}
