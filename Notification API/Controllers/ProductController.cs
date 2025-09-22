using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Notification_API.Hubs;
using Notification_API.Models;
using Notification_API.Services;

namespace Notification_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IHubContext<ProductHub> _hubContext;

        public ProductController(IProductService productService, IHubContext<ProductHub> hubContext)
        {
            _productService = productService;
            _hubContext = hubContext;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            var newProduct = _productService.AddProduct(product);

            // Notify clients
            await _hubContext.Clients.All.SendAsync("ProductCreated", newProduct);

            return Ok(newProduct);
        }

        [HttpGet("{id}")]
        public string GetProduct(int id)
        {
            //var product = _productService.GetProductById(id);
            //return product == null ? NotFound() : Ok(product);
            return "The get function is working id: "  + id;
        }

        [HttpGet("Products")]
        public IActionResult GetProducts()
        {
            return Ok(_productService.GetAllProducts());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            product.Id = id;
            var updated = _productService.UpdateProduct(product);

            if (updated == null)
                return NotFound();

            // Notify clients
            await _hubContext.Clients.All.SendAsync("ProductUpdated", updated);

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var success = _productService.DeleteProduct(id);

            if (!success)
                return NotFound();

            // Notify clients
            await _hubContext.Clients.All.SendAsync("ProductDeleted", id);

            return Ok();
        }

        //[HttpGet("{id}")]
        //public string GetProduct()
        //{
        //    return "Http GetProduct Request";
        //}

        //[HttpGet("Products")]
        //public string GetProducts()
        //{
        //    return "Http GetProducts Request";
        //}

        //[HttpPost]
        //public string CreateProduct(string product)
        //{
        //    return "Http" + product + "Request";
        //}

        //[HttpPut("{id}")]
        //public string UpdateProduct(int id, Product product)
        //{
        //    return "Http UpdateProduct Request";
        //}

        //[HttpDelete("{id}")]
        //public string DeleteProduct(int Id)
        //{
        //    return "Http DeleteProduct Request";
        //}


    }
}
