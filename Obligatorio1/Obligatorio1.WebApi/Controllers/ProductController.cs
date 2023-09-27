using Microsoft.AspNetCore.Mvc;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.Domain;
using Serilog;

namespace Obligatorio1.WebApi
{
    [ApiController]
    [Route("api/products")]

    
    public class ProductController: ControllerBase
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
                Log.Information("Intentando registrar el producto: {@Product}",product);
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
         Summary = "Obtiene la lista de usuarios",
         Description = "Obtiene todos los usuarios registrados en el sistema.")]

    }
}