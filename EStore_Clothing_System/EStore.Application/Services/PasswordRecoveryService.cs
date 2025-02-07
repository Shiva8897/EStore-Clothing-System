using AutoMapper;
using EStore.Application.Interfaces;
using EStore.Domain.Entities;
using EStore.Domain.EntityDtos.NewFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Services
{
     public  class PasswordRecoveryService:IPasswordRecoveryService
    {
        private readonly IEmailService _emailService;
        private readonly IUserService _userService; 
        private readonly ILoginService _loginService;
        private readonly InMemoryTokenStore _inMemoryTokenStore;
        private readonly IMapper _mapper;
        public PasswordRecoveryService(IEmailService emailService,IUserService userService,IMapper mapper ,ILoginService loginService, InMemoryTokenStore inMemoryTokenStore)
        {
            _emailService = emailService;
            _userService = userService;
            _loginService = loginService;
            _inMemoryTokenStore= inMemoryTokenStore;
            _mapper = mapper;
        }
        public async Task<bool> SendResetLinkAsync(string email)
        {
            var user = await _userService.GetUserByEmail(email);
            if (user == null) { return false; }
            var token = _loginService.GeneratePasswordResetToken(user);
            _inMemoryTokenStore.StoreToken(email, token, TimeSpan.FromHours(1));
            var resetLink = $"http://localhost:3000/passwordReset?token={token}&email={email}";

            var message = $"<p>To reset your password, click the link below:</p><p><a href='{resetLink}'>Reset Password</a></p><p>This Link expires in 30 Minutes</p>";
            _emailService.SendMailNotification(email, "Password Reset", message);
            return true;
        }


        public async Task<bool> ResetPasswordAsync(string email, string token, string password)
        {
            var isValidToken = await ValidateTokenAsync(email, token);
            if (!isValidToken)
            {
                return false;
            }
            var user = await _userService.GetUserByEmail(email);
            if (user == null)
            {
                return false;
            }
            user.PasswordHash = password;
            await _userService.UpdateUserPassword(user);
            _inMemoryTokenStore.InvalidateToken(email);
            return true;
        }


        private async Task<bool> ValidateTokenAsync(string email, string token)
        {
            var storedTokenInfo = _inMemoryTokenStore.GetToken(email);
            if (storedTokenInfo == null)
            {
                return false; // No token found for this email
            }

            // Check if the provided token matches the stored token 
            if (storedTokenInfo.Value.Token != token)
            {
                return false; // Tokens do not match
            }

            // Check if the token is expired
            if (DateTime.UtcNow > storedTokenInfo.Value.Expiration)
            {
                return false; // Token has expired
            }

            return true; // Token is valid
        }
    }
}
