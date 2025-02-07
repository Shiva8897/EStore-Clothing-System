using AutoMapper;
using EStore.Application.Interfaces;
using EStore.Application.IRepositories;
using EStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Services
{
    public class WishlistService : IWishlistService
    {

        private readonly IWishlistRepository _wishlistRepository;    
        public WishlistService(IWishlistRepository wishlistRepository)
        {
            _wishlistRepository= wishlistRepository;
        }
        public async Task<Product> AddToWishlistAsync(int userId, int productId)
        {
           return await _wishlistRepository.AddToWishlistAsync(userId, productId);
        }

        public async Task<List<Product>> GetWishlistByUserIdAsync(int userId)
        {
            return await _wishlistRepository.GetWishlistByUserIdAsync(userId);
        }

        public async Task<bool> RemoveFromWishlistAsync(int userId, int productId)
        {
          return await _wishlistRepository.RemoveFromWishlistAsync(userId,productId);    
        }
    }
}
