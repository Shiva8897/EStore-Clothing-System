
using EStore.Application.IRepositories;
using EStore.Domain.Entities;
using EStore.Domain.EntityDtos;
using EStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EStore.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private EStoreDbContext _dbContext;
        public ProductRepository(EStoreDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _dbContext.Products
                .Include(pv => pv.ProductVariants)
                .ToListAsync();

        }

        public async Task<Product> GetProductsByIdAsync(int productId)
        {
            return await _dbContext.Products.Include(pv => pv.ProductVariants).
                FirstOrDefaultAsync(x => x.ProductId == productId);
        }

        public async Task<IEnumerable<Product>> SearchProductAsync(string keyword)
        {
            return await _dbContext.Products
                .Where(p => p.Name.Contains(keyword) || p.ShortDescription.Contains(keyword) || p.LongDesrciption.Contains(keyword) || p.Brand.Contains(keyword))
                .Include(p => p.Category)
                .Include(p => p.SubCategory)
                .Include(p => p.ProductVariants)
                .ToListAsync();
        }

        public async Task AddProductAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
        }
      
        public async Task AddProductWithVariantAsync(Product product)
        {
            // Safeguard to initialize the collection if it is null
            if (product.ProductVariants == null)
            {
                product.ProductVariants = new List<ProductVariant>();
            }


            await _dbContext.Products.AddAsync(product);
            foreach (var productVariant in product.ProductVariants)
            {
                await _dbContext.ProductVariants.AddAsync(productVariant);
            }

            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateProductWithVariantAsync(Product product)
        {
            /*  if (product.ProductVariants == null)
              {
                  product.ProductVariants = new List<ProductVariant>();
              }

              _dbContext.Products.Update(product);
              foreach (var productVariant in product.ProductVariants)
              {
                  await _dbContext.ProductVariants.AddAsync(productVariant);
              }
              await _dbContext.SaveChangesAsync();*/
            var existingProduct = await _dbContext.Products
         .Include(p => p.ProductVariants)
         .FirstOrDefaultAsync(p => p.ProductId == product.ProductId);

            if (existingProduct == null)
            {
                throw new Exception("Product not found");
            }
                _dbContext.Entry(existingProduct).CurrentValues.SetValues(product);
            
            // Update the main product details
            

            // Handle variants
            foreach (var variant in product.ProductVariants)
            {
                var existingVariant = existingProduct.ProductVariants
                    .FirstOrDefault(v => v.ProductVariantId == variant.ProductVariantId);

                if (existingVariant != null)
                {
                    // Update existing variant
                    _dbContext.Entry(existingVariant).CurrentValues.SetValues(variant);
                }
                else
                {
                    // Add new variant
                    await _dbContext.ProductVariants.AddAsync(variant);
                }
            }

            // Remove any variants that are not in the updated product
            foreach (var existingVariant in existingProduct.ProductVariants)
            {
                if (!product.ProductVariants.Any(v => v.ProductVariantId == existingVariant.ProductVariantId))
                {
                    _dbContext.ProductVariants.Remove(existingVariant);
                }
            }

            // Save all changes
            await _dbContext.SaveChangesAsync();
        }
        public async Task AddProductVariantAsync(ProductVariant productVariant)
        {
            await _dbContext.ProductVariants.AddAsync(productVariant);
           await _dbContext.SaveChangesAsync();
        }
      
        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _dbContext.Products
                .Where(p => p.CategoryId == categoryId)
                .Include(p => p.ProductVariants)
                .Include(p => p.Category)
                .Include(p => p.SubCategory)
                .ToListAsync();
        }

        public async Task DeleteProductAsync(int productId)
        {
            var product = await _dbContext.Products.FindAsync(productId);
            if (product != null)
            {
                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync();
            }
        }

        /*public async Task UpdateProductAsync(Product product)
        {
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
        }*/
        public async Task UpdateProductAsync(Product product)
        {
            var existingProduct = await _dbContext.Products
                .Include(p => p.ProductVariants) // Include the ProductVariants
                .FirstOrDefaultAsync(p => p.ProductId == product.ProductId);

            if (existingProduct != null)
            {
                // Update Product fields
                _dbContext.Entry(existingProduct).CurrentValues.SetValues(product);

                // Update or add ProductVariants
                foreach (var variant in product.ProductVariants)
                {
                    var existingVariant = existingProduct.ProductVariants
                        .FirstOrDefault(v => v.ProductVariantId == variant.ProductVariantId);

                    if (existingVariant != null)
                    {
                        // Update the existing variant
                        _dbContext.Entry(existingVariant).CurrentValues.SetValues(variant);
                    }
                    else
                    {
                        // Add new variant
                        existingProduct.ProductVariants.Add(variant);
                    }
                }

                // Handle removed variants
                foreach (var existingVariant in existingProduct.ProductVariants.ToList())
                {
                    if (!product.ProductVariants.Any(v => v.ProductVariantId == existingVariant.ProductVariantId))
                    {
                        // Remove the variant that no longer exists in the updated product
                        _dbContext.ProductVariants.Remove(existingVariant);
                    }
                }

                await _dbContext.SaveChangesAsync();
            }
        }


        public async Task<IEnumerable<Product>> GetFilteredAndSortedProducts(
           int categoryId,
           decimal? minPrice,
           decimal? maxPrice,
           string size,
           string color,
           string sortOrder)
        {
            // Start query by filtering by category
            var query = _dbContext.Products
                .Where(p => p.CategoryId == categoryId)
                .Include(p => p.ProductVariants)
                .AsQueryable();

            // Apply price range filters if provided
            if (minPrice.HasValue)
            {
                query = query.Where(p => p.ProductVariants.Any(v => v.PricePerUnit >= minPrice.Value));
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.ProductVariants.Any(v => v.PricePerUnit <= maxPrice.Value));
            }

            // Apply size filter only if size is provided
            if (!string.IsNullOrEmpty(size))
            {
                var sizes = size.Split(',');
                query = query.Where(p => p.ProductVariants.Any(v => sizes.Contains(v.Size)));
            }

            // Apply color filter only if color is provided
            if (!string.IsNullOrEmpty(color))
            {
                var colors = color.Split(',');
                query = query.Where(p => p.ProductVariants.Any(v => colors.Contains(v.Color)));
            }
         
            query = sortOrder switch
            {
                "price_asc" => query.OrderBy(p => p.ProductVariants.Min(v => v.PricePerUnit)),
                "price_desc" => query.OrderByDescending(p => p.ProductVariants.Max(v => v.PricePerUnit)),
                _ => query,
            };

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<ProductVariant>> GetProductVariants()
        {
            var productVariants = await _dbContext.ProductVariants.ToListAsync();
            return productVariants;
        }

        public async Task<ProductRespDto> GetProductByVariantIdAsync(int productVariantId)
        {
            var productVariant = await _dbContext.ProductVariants
                .Include(pv => pv.Product)
                    .ThenInclude(p => p.Category)
                .Include(pv => pv.Product)
                    .ThenInclude(p => p.SubCategory)
                .FirstOrDefaultAsync(pv => pv.ProductVariantId == productVariantId);
            if (productVariant == null)
            {
                return null;
            }
              var productResDto = new ProductRespDto
              {
                ProductId = productVariant.Product.ProductId,
                Name = productVariant.Product.Name,
                ShortDescription = productVariant.Product.ShortDescription,
                ImageData = Convert.ToBase64String(productVariant.Product.ImageData)
              };
          
            return productResDto;
        }

        public async Task<IEnumerable<Product>> GetProductsByPriceRangeAsync(
           int categoryId,
           decimal? minPrice,
           decimal? maxPrice)
        {
            var query = _dbContext.Products
                .Where(p => p.CategoryId == categoryId)
                .Include(p => p.ProductVariants)
                .AsQueryable();

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.ProductVariants.Any(v => v.PricePerUnit >= minPrice.Value));
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.ProductVariants.Any(v => v.PricePerUnit <= maxPrice.Value));
            }

            return await query.ToListAsync();
        }

       

        public async Task UpdateProductVariantAsync(ProductVariant productVariant)
        {
            _dbContext.ProductVariants.Update(productVariant);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductVariant>> GetProductVariantsByProductIdAsync(int productId)
        {
            return await _dbContext.ProductVariants
                .Where(pv => pv.ProductId == productId)
                .ToListAsync();
        }

        public async Task DeleteProductVariantAsync(int productVariantId)
        {
            var variant = await _dbContext.ProductVariants.FindAsync(productVariantId);
            if (variant != null)
            {
                _dbContext.ProductVariants.Remove(variant);
                await _dbContext.SaveChangesAsync();
            }
        }

    }
}
    









