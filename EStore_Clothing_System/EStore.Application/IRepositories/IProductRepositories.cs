using EStore.Domain.Entities;
using EStore.Domain.EntityDtos;

namespace EStore.Application.IRepositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductsByIdAsync(int id);
        Task<IEnumerable<Product>> SearchProductAsync(string keyword);
        Task AddProductAsync(Product product);
        Task DeleteProductAsync(int productId);
        Task UpdateProductAsync(Product product);
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
        Task<IEnumerable<Product>> GetFilteredAndSortedProducts(
            int categoryId,
            decimal? minPrice,
            decimal? maxPrice,
            string size,
            string color,
            string sortOrder);
        Task<IEnumerable<ProductVariant>> GetProductVariants();
        Task<ProductRespDto> GetProductByVariantIdAsync(int productVariantId);
        Task<IEnumerable<Product>> GetProductsByPriceRangeAsync(
          int categoryId,
          decimal? minPrice,
          decimal? maxPrice);

        Task AddProductWithVariantAsync(Product product);
        Task AddProductVariantAsync(ProductVariant productVariant);
        Task UpdateProductWithVariantAsync(Product product);
        Task UpdateProductVariantAsync(ProductVariant productVariant);
        Task<IEnumerable<ProductVariant>> GetProductVariantsByProductIdAsync(int productId);
        Task DeleteProductVariantAsync(int productVariantId);
    }
   
}
