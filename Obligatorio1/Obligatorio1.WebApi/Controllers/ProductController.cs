using Microsoft.AspNetCore.Mvc;
using Obligatorio1.Domain;
using Obligatorio1.Exceptions;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.WebApi.Filters;
using Serilog;

namespace Obligatorio1.WebApi
{
    [ApiController]
    [Route("api/products")]

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
        [TypeFilter(typeof(AuthenticationFilter))]
        [TypeFilter(typeof(AuthorizationRolFilter))]
        [TypeFilter(typeof(ExceptionFilter))]
        [HttpPost]
        public IActionResult RegisterProduct([FromBody] Product product)
        {
            _productService.RegisterProduct(product);
            return Ok("Producto registrado correctamente");
        }

        /// <summary>
        /// Obtains the list of registered products in the system.
        /// </summary>
        /// <returns> HTTP response with the list of products.</returns>
        [TypeFilter(typeof(AuthenticationFilter))]
        [TypeFilter(typeof(AuthorizationRolFilter))]
        [TypeFilter(typeof(ExceptionFilter))]
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _productService.GetProducts();
            return Ok(products);
        }

        /// <summary>
        /// Obtains a product by its ID.
        /// </summary>
        /// <param name="id">ID from the product searched.</param>
        /// <returns> HTTP response with the required product.</returns>
        [TypeFilter(typeof(AuthenticationFilter))]
        [TypeFilter(typeof(AuthorizationRolFilter))]
        [TypeFilter(typeof(ExceptionFilter))]
        [HttpGet("{id}")]
        public IActionResult GetProductByID([FromRoute] int id)
        {
            var product = _productService.GetProductByID(id);
            return Ok(product);
        }

        /// <summary>
        /// Deletes a product by ID.
        /// </summary>
        /// <param name="id">ID from the product to delete.</param>
        /// <returns> HTTP response and indicates de result of the delete.</returns>
        /// /// <response code="204">The product was deleted successfully.</response>
        /// <response code="404">The product with ID wasnt able to be found.</response>
        /// <response code="400">Error with the request.</response>
        [TypeFilter(typeof(AuthenticationFilter))]
        [TypeFilter(typeof(AuthorizationRolFilter))]
        [TypeFilter(typeof(ExceptionFilter))]
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct([FromRoute] int id)
        {
            _productService.DeleteProduct(id);
            return Ok("Producto eliminado correctamente.");
        }

        /// <summary>
        /// Updates a product by its ID.
        /// </summary>
        /// <param name="product">The updated product information.</param>
        /// <returns>HTTP response indicating the result of the update operation.</returns>
        /// <response code="204">The product was updated successfully.</response>
        /// <response code="404">The product with the specified ID was not found.</response>
        /// <response code="400">Error with the request.</response>
        [TypeFilter(typeof(AuthenticationFilter))]
        [TypeFilter(typeof(AuthorizationRolFilter))]
        [TypeFilter(typeof(ExceptionFilter))]
        [HttpPut("UpdateProduct")]
        public IActionResult UpdateProduct([FromBody] Product product)
        {
            _productService.UpdateProduct(product);
            return Ok("Producto actualizado correctamente.");
        }
    }
}