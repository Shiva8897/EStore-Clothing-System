using AutoMapper;
using EStore.Application.Interfaces;
using EStore.Application.IRepositories;
using EStore.Domain.Entities;
using EStore.Domain.EntityDtos;
using EStore.Domain.EntityDtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        public UserService(IUserRepository userRepository, IMapper mapper, IEmailService emailService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _emailService = emailService;
        }
                
        public async Task<User> GetUserByEmail(string email)
        {

            return await _userRepository.GetUserByEmail(email);
        }

        public async Task<User> RegisterUser(UserReq user)
        {
            var userDto= _mapper.Map<User>(user);
            userDto.Role = "User";  
            userDto.CreatedDate = DateTime.Now;
            return await _userRepository.RegisterUser(userDto);

        }
        public async Task<User> UpdateUserPassword(User user)
        {
            return await _userRepository.UpdateUserPassword(user);
        }
        public async Task ShareOrderDetailsViaEmail(EmailReq emailReq)
        {
            _emailService.SendMailNotification(emailReq.ToEmail,emailReq.Subject,emailReq.Body);
        }
    }
}
