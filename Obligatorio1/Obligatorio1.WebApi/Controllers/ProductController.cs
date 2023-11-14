using Microsoft.AspNetCore.Mvc;
using Obligatorio1.Domain;
using Obligatorio1.Exceptions;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.WebApi.Filters;
using Serilog;
using Microsoft.AspNetCore.Http;

namespace Obligatorio1.WebApi
{
    [ApiController]
    [Route("api/products")]
    [TypeFilter(typeof(ExceptionFilter))]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Registers a new product in the system.
        /// </summary>
        /// <param name="product">Product data to register.</param>
        /// <returns> HTTP response with the register result.</returns>

        [HttpPost]
        [TypeFilter(typeof(AuthenticationFilter))]
        [TypeFilter(typeof(AuthorizationRolFilter))]
        public IActionResult RegisterProduct([FromBody] Product product)
        {
            try
            {
                _productService.RegisterProduct(product);
                // Si el registro se realiza correctamente, devolvemos una respuesta de éxito
                var response = new { message = "Product registered correctly." };
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Si ocurre un error, devolvemos una respuesta de error con información sobre el error
                var errorResponse = new { error = "An error occurred while registering the product.", details = ex.Message };
                return BadRequest(errorResponse);
            }
        }


        /// <summary>
        /// Obtains the list of registered products in the system.
        /// </summary>
        /// <returns> HTTP response with the list of products.</returns>
        [HttpGet]
        [TypeFilter(typeof(AuthenticationFilter))]
        [TypeFilter(typeof(AuthorizationRolFilter))]
        public IActionResult GetProducts()
        {
            try
            {
                var products = _productService.GetProducts();
                if (products != null)
                {
                    return Ok(products);
                }
                else
                {
                    return NotFound(new { message = "No products found" });
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return BadRequest(new { message = "Error: " + ex.Message });
            }
        }

        /// <summary>
        /// Obtains the list of registered products in the system.
        /// </summary>
        /// <returns> HTTP response with the list of products.</returns>
        [HttpGet("GetProducts")]
        public IActionResult GetProductss()
        {
            try
            {
                var products = _productService.GetProducts();
                if (products != null)
                {
                    return Ok(products);
                }
                else
                {
                    return NotFound(new { message = "No products found" });
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return BadRequest(new { message = "Error: " + ex.Message });
            }
        }

        /// <summary>
        /// Obtains a product by its ID.
        /// </summary>
        /// <param name="id">ID from the product searched.</param>
        /// <returns> HTTP response with the required product.</returns>
        [HttpGet("{id}")]

        public IActionResult GetProductByID([FromRoute] int id)
        {
            try
            {
                var product = _productService.GetProductByID(id);
                if (product != null)
                {
                    return Ok(product);
                }
                else
                {
                    return NotFound(new { message = "Product not found" });
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return BadRequest(new { message = "Error: " + ex.Message });
            }
        }


        /// <summary>
        /// Deletes a product by ID.
        /// </summary>
        /// <param name="id">ID from the product to delete.</param>
        /// <returns> HTTP response and indicates de result of the delete.</returns>
        /// /// <response code="204">The product was deleted successfully.</response>
        /// <response code="404">The product with ID wasnt able to be found.</response>
        /// <response code="400">Error with the request.</response>
        [HttpDelete("{id}")]
        [TypeFilter(typeof(AuthenticationFilter))]
        [TypeFilter(typeof(AuthorizationRolFilter))]
        public IActionResult DeleteProduct([FromRoute] int id)
        {
            try
            {
                _productService.DeleteProduct(id);
                return Ok(new { message = "Product disposed correctly." });
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return BadRequest(new { message = "Error: " + ex.Message });
            }
        }

        /// <summary>
        /// Updates a product by its ID.
        /// </summary>
        /// <param name="product">The updated product information.</param>
        /// <returns>HTTP response indicating the result of the update operation.</returns>
        /// <response code="204">The product was updated successfully.</response>
        /// <response code="404">The product with the specified ID was not found.</response>
        /// <response code="400">Error with the request.</response>
        [HttpPut("{id}")]
        [TypeFilter(typeof(AuthenticationFilter))]
        [TypeFilter(typeof(AuthorizationRolFilter))]
        public IActionResult UpdateProduct([FromRoute] int id, [FromBody] Product product)
        {
            try
            {
                _productService.UpdateProduct(id, product);
                var response = new { message = "Successfully updated product" };
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return BadRequest(new { message = "Error: " + ex.Message });
            }
        }

    }
}