using EStore.Application.Interfaces;
using EStore.Application.Services;
using EStore.Domain.Entities;
using EStore.Domain.EntityDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using System.Security.Policy;

namespace EStore.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int productId)
        {
            try
            {
                var productDto = await _productService.GetProductByIdAsync(productId);
                return Ok(productDto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchProduct([FromQuery] string keyword)
        {
            var products = await _productService.SearchProductAsync(keyword);
            return Ok(products);
        }
   
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm] CreateProductDto createProductDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdProductId = await _productService.AddProductAsync(createProductDto);

            return CreatedAtAction(nameof(GetProductById), new { productId = createdProductId }, createProductDto);
        }
        [Authorize]
        [HttpPost("ProductWihVariant")]
        public async Task<IActionResult> AddProductWithVariant([FromForm] AddProductDto addProductDto, [FromForm] string addProductVariantsJson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Deserialize the product variants from JSON
                var addProductVariantDtos = JsonConvert.DeserializeObject<List<AddProductVariantDto>>(addProductVariantsJson);
                addProductDto.addProductVariantDtos = addProductVariantDtos;

                // Call the service to add the product and its variants
                var productId = await _productService.AddProductWithVariantAsync(addProductDto);

                // Return a success response with the created product's ID
                return Ok(new { ProductId = productId, Message = "Product with variants added successfully" });
            }
            catch (Exception ex)
            {
                // Handle any errors
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [Authorize]
        [HttpPut("EditProductVariant{productId}")]
        public async Task<IActionResult> UpdateProductWithVariantAsync(int productId, [FromForm] AddProductDto addProductDto, [FromForm] string addProductVariantsJson)
        {
            var existingProduct = await _productService.GetProductByIdAsync(productId);
            if (existingProduct == null)
            {
                return NotFound($"Product with ID {productId} not found.");
            }
            if (addProductDto == null)
            {
                return BadRequest("Update data is required.");
            }
            try
            {
                var addProductVariantDtos = JsonConvert.DeserializeObject<List<AddProductVariantDto>>(addProductVariantsJson);
                addProductDto.addProductVariantDtos = addProductVariantDtos;

                await _productService.UpdateProductWithVariantAsync(productId, addProductDto);

                return Ok(addProductDto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize]
        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            try
            {
                await _productService.DeleteProductAsync(productId);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromForm] UpdateProductDto updateProductDto)
        {
            if (updateProductDto == null)
            {
                return BadRequest("Update data is required.");
            }
            try
            {
                await _productService.UpdateProductAsync(productId, updateProductDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCategory(int categoryId)
        {
            var products = await _productService.GetProductsByCategoryAsync(categoryId);
            return Ok(products);
        }
        [HttpGet("category/{categoryId}/filter")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetFilteredAndSortedProducts(
           int categoryId,
           [FromQuery] decimal? minPrice,
           [FromQuery] decimal? maxPrice,
           [FromQuery] string size,
           [FromQuery] string color,
           [FromQuery] string sortOrder)
        {
            try
            {
                var products = await _productService.GetFilteredAndSortedProductsAsync(
                    categoryId, minPrice, maxPrice, size, color, sortOrder
                );

                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred while fetching products.", error = ex.Message });
            }
        }


        [HttpGet]
        [Route("ProductVariants")]
        public async Task<IActionResult> GetProductVariantId()
        {
            try
            {
                var productVariant = await _productService.GetProductVariants();
                if (productVariant == null)
                {
                    return NotFound($"Unable to fetch the product variants");
                }
                return Ok(productVariant);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
        [HttpGet("variant/{productVariantId}")]
        public async Task<ActionResult<ProductRespDto>> GetProductByVariantId(int productVariantId)
        {
            try
            {
                var productRespDto = await _productService.GetProductByVariantIdAsync(productVariantId);
                if (productRespDto == null)
                {
                    return NotFound($"Product variant with ID {productVariantId} not found.");
                }
                return Ok(productRespDto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Product variant with ID {productVariantId} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("category/{categoryId}/filterbyPrice")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetFilteredAndSortedProducts(
           int categoryId,
           [FromQuery] decimal? minPrice,
           [FromQuery] decimal? maxPrice)
        {
            try
            {
                var products = await _productService.GetProductsByPriceRangeAsync(categoryId, minPrice, maxPrice);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred while fetching products.", error = ex.Message });
            }
        }

    }

}
