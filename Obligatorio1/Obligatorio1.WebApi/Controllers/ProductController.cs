using Microsoft.AspNetCore.Mvc;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.Domain;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;
using Obligatorio1.Exceptions;
using Obligatorio1.BusinessLogic;

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
        [HttpPost]

        public IActionResult RegisterProduct([FromBody] Product product)
        {
            try
            {
                Log.Information("Intentando registrar el producto: {@Product}", product);
                _productService.RegisterProduct(product);
                Log.Information("Producto registrado exitosamente");
                return Ok("Producto registrado correctamente");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al registrar el producto", ex.Message);
                return BadRequest($"Error al registrar el usuario: {ex.Message}");
            }
        }


        /// <summary>
        /// Obtains the list of registered products in the system.
        /// </summary>
        /// <returns> HTTP response with the list of products.</returns>
        [HttpGet]
        [SwaggerOperation(
         Summary = "Obtiene la lista de productos",
         Description = "Obtiene todos los productos registrados en el sistema.")]
        [ProducesResponseType(typeof(IEnumerable<Product>), 200)]
        [ProducesResponseType(typeof(string), 400)]

        public IActionResult GetProducts()
        {
            try
            {
                var products = _productService.GetProducts();
                return Ok(products);
            }
            catch (ProductManagmentException ex)
            {
                Log.Error(ex, $"Error al obtener productos: {ex.Message}");
                return BadRequest($"Error al obtener productos: {ex.Message}");
            }
            catch (Exception e)
            {
                Log.Error(e, "Error inesperado al obtener los productos: {ErrorMessage}", e.Message);
                return BadRequest($"Error inesperado al obtener los productos: {e.Message}");
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
                if (product == null)
                {
                    return NotFound($"Producto con ID {id} no encontrado");


                }
                return Ok(product);
            }
            catch (ProductManagmentException e)
            {
                Log.Error(e, "Error al obtener el producto: {ErrorMessage}", e.Message);
                return BadRequest($"Error al obtener el producto: {e.Message}");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error inesperado al obtener el usuario: {ErrorMessage}", ex.Message);
                return BadRequest($"Error inesperado al obtener el producto: {ex.Message}");
            }
        }

        /// <summary>
        /// Creates a new prduct in the system.
        /// </summary>
        /// <param name="product">The data of the product.</param>
        /// <returns> HTTP response with the created product.</returns>
        [HttpPost("create")]
        public IActionResult CreateProduct([FromBody] Product product)
        {

            try
            {
                Log.Information("Intentando crear un nuevo producto: {@Product}", product);
                var createProduct = _productService.CreateProduct(product);

                Log.Information("Producto creado exitosamente: {@CreatedProduct}", createProduct);

                return CreatedAtAction(nameof(GetProductByID), new { id = createProduct.ProductID }, createProduct);
            }
            catch (ProductManagmentException ex)
            {
                Log.Error(ex, "Error al crear el producto: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception e)
            {
                Log.Error(e, "Error inesperado al crear el producto: {ErrorMessage}", e.Message);

                return BadRequest($"Error inesperado al crear el producto: {e.Message}");
            }
        }

        /// <summary>
        /// Deletes a product by ID.
        /// </summary>
        /// <param name="id">ID from the product to delete.</param>
        /// <returns> HTTP response and indicates de result of the delete.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct([FromRoute] int id)
        {
            try
            {
                Log.Information("Intentando eliminar el producto con ID: {UserID}", id);


                _productService.DeleteProduct(id);

                Log.Information("Producto eliminado exitosamente con ID: {UserID}", id);

                return NoContent();
            }
            catch (ProductManagmentException ex)
            {
                Log.Error(ex, "Error al eliminar el producto: {ErrorMessage}", ex.Message);

                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error inesperado al eliminar el producto: {ErrorMessage}", ex.Message);

                return BadRequest($"Error inesperado al eliminar el producto: {ex.Message}");
            }
        }
    }
}