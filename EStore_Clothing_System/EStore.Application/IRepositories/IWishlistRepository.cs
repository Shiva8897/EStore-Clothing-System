using EStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.IRepositories
{
    public  interface IWishlistRepository
    {
        Task<Product> AddToWishlistAsync(int userId, int productId);
        Task<bool> RemoveFromWishlistAsync(int userId, int productVariantId);
        Task<List<Product>> GetWishlistByUserIdAsync(int userId);
    }
}
