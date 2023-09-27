using Microsoft.AspNetCore.Mvc;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.Domain;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;

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

        [HttpGet]
        [SwaggerOperation(
         Summary = "Obtiene la lista de productos",
         Description = "Obtiene todos los productos registrados en el sistema.")]
        [ProducesResponseType(typeof(IEnumerable<Product>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult GetProduct()
        {
            try
            {
                var products = _productService.GetProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener productos: {ex.Message}");
            }
        }

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
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener el producto: {ex.Message}");
            }
        }
    }
}