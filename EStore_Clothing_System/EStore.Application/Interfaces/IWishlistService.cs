using EStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Interfaces
{
     public  interface IWishlistService
     {
        Task<Product> AddToWishlistAsync(int userId, int productId);
        Task<bool> RemoveFromWishlistAsync(int userId, int productVariantId);
        Task<List<Product>> GetWishlistByUserIdAsync(int userId);
    }
}
