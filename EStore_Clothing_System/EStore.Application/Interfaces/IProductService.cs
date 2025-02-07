using EStore.Domain.Entities;
using EStore.Domain.EntityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<IEnumerable<ProductDto>> SearchProductAsync(string keyword);
        Task<int> AddProductAsync(CreateProductDto createProductDto);
        Task DeleteProductAsync(int productId);
        Task UpdateProductAsync(int productId, UpdateProductDto updateProductDto);
        Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId);
        Task<IEnumerable<ProductVariant>> GetProductVariants();
        Task<ProductRespDto> GetProductByVariantIdAsync(int productVariantId);
       Task<IEnumerable<ProductDto>> GetFilteredAndSortedProductsAsync(
             int categoryId,
             decimal? minPrice,
             decimal? maxPrice,
             string size,
             string color,
             string sortOrder);

        Task<IEnumerable<ProductDto>> GetProductsByPriceRangeAsync(
         int categoryId,
         decimal? minPrice,
         decimal? maxPrice);

        Task<int> AddProductWithVariantAsync(AddProductDto addProductDto);

        Task UpdateProductWithVariantAsync(int productId,AddProductDto addProductDto);
        Task AddProductVariantAsync(ProductVariantDto productVariantDto);
       
    }
}
