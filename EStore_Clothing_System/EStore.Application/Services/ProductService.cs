
using AutoMapper;
using EStore.Application.Interfaces;
using EStore.Application.IRepositories;
using EStore.Domain.Entities;
using EStore.Domain.EntityDtos;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllProducts();
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

            foreach (var productDto in productDtos)
            {
                var product = products.FirstOrDefault(p => p.ProductId == productDto.ProductId);
                if (product?.ImageData != null)
                {
                    productDto.ImageBase64 = Convert.ToBase64String(product.ImageData);
                }
            }

            return productDtos;
        }

        public async Task<ProductDto> GetProductByIdAsync(int productId)
        {
            if (productId <= 0)
            {
                throw new ArgumentException("Invalid product ID", nameof(productId));
            }

            var product = await _productRepository.GetProductsByIdAsync(productId);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {productId} not found.");
            }

            var productDto = _mapper.Map<ProductDto>(product);

            // Convert image data to base64 string
            if (product.ImageData != null && product.ImageData.Length > 0)
            {
                productDto.ImageBase64 = Convert.ToBase64String(product.ImageData);
            }

            return productDto;

        }
        public async Task<IEnumerable<ProductDto>> SearchProductAsync(string keyword)
        {
            var products = await _productRepository.SearchProductAsync(keyword);
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

            foreach (var productDto in productDtos)
            {
                var product = products.FirstOrDefault(p => p.ProductId == productDto.ProductId);
                if (product?.ImageData != null)
                {
                    productDto.ImageBase64 = Convert.ToBase64String(product.ImageData);
                }
            }

            return productDtos;
        }

        public async Task<int> AddProductAsync(CreateProductDto createProductDto)
        {
            var product = _mapper.Map<Product>(createProductDto);
            if (createProductDto.ImageFile != null && createProductDto.ImageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await createProductDto.ImageFile.CopyToAsync(memoryStream);
                    product.ImageData = memoryStream.ToArray();
                }
            }
            product.CreatedDate = DateTime.UtcNow;
            product.ModifiedDate = DateTime.UtcNow;
            await _productRepository.AddProductAsync(product);
            return product.ProductId;
        }
        /*        public async Task<int> AddProductWithVariantAsync(AddProductDto addProductDto)
                {
                    var product = _mapper.Map<Product>(addProductDto);
                    if (addProductDto.ImageFile != null && addProductDto.ImageFile.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await addProductDto.ImageFile.CopyToAsync(memoryStream);
                            product.ImageData = memoryStream.ToArray();
                        }
                    }
                    // Initialize the ProductVariants collection
                    product.ProductVariants = new List<ProductVariant>();

                    // Map and add Product Variants if provided
                    if (addProductDto.addProductVariantDtos != null && addProductDto.addProductVariantDtos.Any())
                    {
                        // Map DTOs to ProductVariant entities
                        var productVariants = _mapper.Map<List<ProductVariant>>(addProductDto.addProductVariantDtos);

                        // Set the ProductId for each ProductVariant
                        foreach (var variant in productVariants)
                        {
                            variant.ProductId = product.ProductId; // Associate the variant with the created product
                            product.ProductVariants.Add(variant); // Add the variant to the product's variants list
                        }
                    }

                    // Add the product to the repository
                    await _productRepository.AddProductWithVariantAsync(product);
                    return product.ProductId;
                }*/
        public async Task<int> AddProductWithVariantAsync(AddProductDto addProductDto)
        {
            var product = new Product
            {
                Name = addProductDto.Name,
                ShortDescription = addProductDto.ShortDescription,
                LongDesrciption = addProductDto.LongDesrciption,
                Brand = addProductDto.Brand,
                CategoryId = addProductDto.CategoryId,
                SubCategoryId = addProductDto.SubCategoryId,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                
            };

            // Upload and save the product image
            if (addProductDto.ImageFile != null)
            {
                using (var stream = new MemoryStream())
                {
                    addProductDto.ImageFile.CopyTo(stream);
                    product.ImageData = stream.ToArray();
                }
            }

            // Create new product variants
            foreach (var addProductVariantDto in addProductDto.addProductVariantDtos)
            {
                var productVariant = new ProductVariant
                {
                   
                    Name = product.Name,
                    Size = addProductVariantDto.Size,
                    Color = addProductVariantDto.Color,
                    PricePerUnit = addProductVariantDto.PricePerUnit,
                    Quantity = addProductVariantDto.Quantity
                };

                product.ProductVariants.Add(productVariant);
            }

            // Save the product and its variants
            await _productRepository.AddProductWithVariantAsync(product);
            return product.ProductId;
        }
        public async Task UpdateProductWithVariantAsync(int productId,AddProductDto addProductDto)
        {                 
           
            if (addProductDto == null)
            {
                throw new ArgumentNullException(nameof(addProductDto));
            }

            var existingProduct = await _productRepository.GetProductsByIdAsync(productId);

            existingProduct.Name = addProductDto.Name;
            existingProduct.ShortDescription = addProductDto.ShortDescription;
            existingProduct.LongDesrciption = addProductDto.LongDesrciption;
            existingProduct.Brand = addProductDto.Brand;
            existingProduct.CategoryId = addProductDto.CategoryId;
            existingProduct.SubCategoryId = addProductDto.SubCategoryId;
            existingProduct.ModifiedDate = DateTime.UtcNow;

            // Upload and save the product image if provided
            if (addProductDto.ImageFile != null)
            {
                using (var stream = new MemoryStream())
                {
                    await addProductDto.ImageFile.CopyToAsync(stream);
                    existingProduct.ImageData = stream.ToArray();
                }
            }
            else
            {
                // If no new image is provided, retain the existing image
                // This line is optional since existingProduct.ImageData will remain unchanged if not set.
                existingProduct.ImageData = existingProduct.ImageData;
            }

            var existingVariants = existingProduct.ProductVariants.ToList();

            foreach (var addProductVariantDto in addProductDto.addProductVariantDtos)
            {
                // Try to find an existing variant based on size and color
                var existingVariant = existingVariants
                    .FirstOrDefault(v => v.Size == addProductVariantDto.Size && v.Color == addProductVariantDto.Color);

                if (existingVariant != null)
                {
                    // Update existing variant
                    existingVariant.PricePerUnit = addProductVariantDto.PricePerUnit;
                    existingVariant.Quantity = addProductVariantDto.Quantity;
                }
                else
                {
                    // Create a new variant
                    var productVariant = new ProductVariant
                    {
                        Name = existingProduct.Name,
                        Size = addProductVariantDto.Size,
                        Color = addProductVariantDto.Color,
                        PricePerUnit = addProductVariantDto.PricePerUnit,
                        Quantity = addProductVariantDto.Quantity
                    };

                    existingProduct.ProductVariants.Add(productVariant);
                }
            }


            await _productRepository.UpdateProductWithVariantAsync(existingProduct);
        }

        public async Task DeleteProductAsync(int productId)
        {
            var product = await _productRepository.GetProductsByIdAsync(productId);
            if (product == null)
            {
                throw new InvalidOperationException($"Product with ID {productId} not found.");
            }
            await _productRepository.DeleteProductAsync(productId);
        }


        public async Task UpdateProductAsync(int productId, UpdateProductDto updateProductDto)
        {
            if (updateProductDto == null)
            {
                throw new ArgumentNullException(nameof(updateProductDto));
            }

            var existingProduct = await _productRepository.GetProductsByIdAsync(productId);
            if (existingProduct == null)
            {
                throw new KeyNotFoundException($"Product with ID {productId} not found.");
            }

            // Map updated product details
            _mapper.Map(updateProductDto, existingProduct);

            // Handle image upload if available
            if (updateProductDto.ImageFile != null && updateProductDto.ImageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await updateProductDto.ImageFile.CopyToAsync(memoryStream);
                    existingProduct.ImageData = memoryStream.ToArray();
                }
            }

            // Update existing product variants or add new ones
            foreach (var variantDto in updateProductDto.ProductVariants)
            {
                var existingVariant = existingProduct.ProductVariants
                    .FirstOrDefault(v => v.ProductVariantId == variantDto.ProductVariantId);

                if (existingVariant != null)
                {
                    // Update existing variant
                    _mapper.Map(variantDto, existingVariant);
                }
                else
                {
                    // Add new variant
                    var newVariant = _mapper.Map<ProductVariant>(variantDto);
                    existingProduct.ProductVariants.Add(newVariant);
                }
            }

            // Remove deleted variants
            var variantIdsToUpdate = updateProductDto.ProductVariants.Select(v => v.ProductVariantId).ToList();
            foreach (var variant in existingProduct.ProductVariants.ToList())
            {
                if (!variantIdsToUpdate.Contains(variant.ProductVariantId))
                {
                    existingProduct.ProductVariants.Remove(variant);
                }
            }

            // Update product's modified date
            existingProduct.ModifiedDate = DateTime.UtcNow;

            // Save updated product and variants to the database
            await _productRepository.UpdateProductAsync(existingProduct);
        }

        public async Task<IEnumerable<ProductDto>> GetFilteredAndSortedProductsAsync(
           int categoryId,
           decimal? minPrice,
           decimal? maxPrice,
           string size,
           string color,
           string sortOrder)
        {
            size = string.IsNullOrWhiteSpace(size) ? null : size;
            color = string.IsNullOrWhiteSpace(color) ? null : color;

            try
            {
                var products = await _productRepository.GetFilteredAndSortedProducts(
                    categoryId, minPrice, maxPrice, size, color, sortOrder);

                var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
                foreach (var productDto in productDtos)
                {
                    var product = products.FirstOrDefault(p => p.ProductId == productDto.ProductId);
                    if (product?.ImageData != null)
                    {
                        productDto.ImageBase64 = Convert.ToBase64String(product.ImageData);
                    }
                }

                return productDtos;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching products", ex);
            }
        }
        public async Task<IEnumerable<ProductVariant>> GetProductVariants()
        {
            var productVariants = await _productRepository.GetProductVariants();
            return productVariants;
        }

        public async Task<ProductRespDto> GetProductByVariantIdAsync(int productVariantId)
        {
            var productResDto= await _productRepository.GetProductByVariantIdAsync(productVariantId);           
            return productResDto;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsByPriceRangeAsync(
         int categoryId,
         decimal? minPrice,
         decimal? maxPrice)
        {
            try
            {
                var products = await _productRepository.GetProductsByPriceRangeAsync(categoryId, minPrice, maxPrice);
                var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

                // If you need to handle ImageData to ImageBase64 mapping
                foreach (var productDto in productDtos)
                {
                    var product = products.FirstOrDefault(p => p.ProductId == productDto.ProductId);
                    if (product?.ImageData != null)
                    {
                        productDto.ImageBase64 = Convert.ToBase64String(product.ImageData);
                    }
                }

                return productDtos;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching products", ex);
            }
        }


        public async Task AddProductVariantAsync(ProductVariantDto productVariantDto)
        {
            // Map the DTO to entity
            var productVariant = _mapper.Map<ProductVariant>(productVariantDto);

            // Call the repository to add the variant
            await _productRepository.AddProductVariantAsync(productVariant);
        }

        public async Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId)
        {
            var products = await _productRepository.GetProductsByCategoryAsync(categoryId);
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

            foreach (var productDto in productDtos)
            {
                var product = products.FirstOrDefault(p => p.ProductId == productDto.ProductId);
                if (product?.ImageData != null)
                {
                    productDto.ImageBase64 = Convert.ToBase64String(product.ImageData);
                }
            }

            return productDtos;
        }
    }
}
