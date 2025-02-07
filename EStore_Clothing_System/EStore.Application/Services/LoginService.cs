using AutoMapper;
using EStore.Application.Interfaces;
using EStore.Application.IRepositories;
using EStore.Domain.Entities;
using EStore.Domain.EntityDtos;
using EStore.Domain.EntityDtos.OrderDtos;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Services
{
    public class LoginService:ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IMapper _mapper;
        private IConfiguration _Config;
        public LoginService(ILoginRepository loginRepository, IMapper mapper, IConfiguration config)
        {
            _loginRepository = loginRepository;
            _mapper = mapper;
            _Config = config;
        }

        public Task<User> AuthenticateUser(LoginReq loginDetails)
        {
            var logindto=_mapper.Map<User>(loginDetails);   
            var user = _loginRepository.GetUserByCredentails(logindto.Email,logindto.PasswordHash);

            if (user == null)
                return null;
            return user;
        }

        public string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                 var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),  // Use UserId as subject
                    new Claim(JwtRegisteredClaimNames.Email, user.Email), // Email claim
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()), // NameIdentifier for UserId
                    new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName), // User Full Name
                    new Claim(ClaimTypes.Role, user.Role) // User role
                };

            var token = new JwtSecurityToken(
                issuer: _Config["Jwt:Issuer"],
                audience: _Config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public async Task<LoginRes> ProvideToken(LoginReq login)
        {
            var user = await AuthenticateUser(login);

            if (user == null)
            {
                return null;
            }
            var token = GenerateToken(user);
            return new LoginRes { Token = token, Role = user.Role };
        }
        public string GeneratePasswordResetToken(User user)
        {
            var token = GenerateToken(user);
            return token;
        }

    }
}
