using EStore.Application.IRepositories;
using EStore.Domain.Entities;
using EStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EStore.Infrastructure.Repositories
{
    public class WishlistRepository : IWishlistRepository
    {
        private EStoreDbContext _context;
        public WishlistRepository(EStoreDbContext context)
        {
            _context = context;
        }


        public async Task<Product> AddToWishlistAsync(int userId, int productId)
        {
            // Check if the user has a wishlist; if not, create one
            var wishlist = await _context.WishList
                .Include(w => w.Product) // Include Products
                .FirstOrDefaultAsync(w => w.UserId == userId);

            if (wishlist == null)
            {
                wishlist = new WishList
                {
                    UserId = userId,
                    CreatedDate = DateTime.UtcNow,
                    Product = new List<Product>() // Initialize the list
                };

                _context.WishList.Add(wishlist);
            }

            // Check if the product is already in the wishlist
            var existingProduct = wishlist.Product
                .FirstOrDefault(p => p.ProductId == productId);

            if (existingProduct != null)
            {
                return existingProduct; // The product already exists in the wishlist
            }

            // Retrieve the product to be added
            var productToAdd = await _context.Products.FindAsync(productId);
            if (productToAdd == null)
            {
                return null; // Product does not exist
            }

            // Add the product to the wishlist
            wishlist.Product.Add(productToAdd); // Add the product to the wishlist

            await _context.SaveChangesAsync();

            return productToAdd;
        }

        public async Task<bool> RemoveFromWishlistAsync(int userId, int productId)
        {
            // Get the user's wishlist
            var wishlist = await _context.WishList
                .Include(w => w.Product) // Include Products
                .FirstOrDefaultAsync(w => w.UserId == userId);

            if (wishlist == null)
            {
                return false; // No wishlist found for the user
            }

            // Find the product to remove
            var productToRemove = wishlist.Product
                .FirstOrDefault(p => p.ProductId == productId);

            if (productToRemove == null)
            {
                return false; // Product not found in the wishlist
            }

            wishlist.Product.Remove(productToRemove); // Remove the product from the wishlist

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Product>> GetWishlistByUserIdAsync(int userId)
        {
            // Get the user's wishlist
            var wishlist = await _context.WishList
                .Include(w => w.Product) // Include Products
                .FirstOrDefaultAsync(w => w.UserId == userId);

            return wishlist?.Product.ToList(); // Return the list of products
        }

    }
}

