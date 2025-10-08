using ECommerce_Case_Study.Data.Models;
using ECommerce_Case_Study.DTOs;
using ECommerce_Case_Study.Repositories.GenericRepository;
using ECommerce_Case_Study.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_Case_Study.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductRepo _productRepo;
        private readonly ICategoryRepo _categoryRepo;

        public ProductController(IProductRepo productRepo, ICategoryRepo categoryRepo)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
        }

        [HttpGet("min/{min}/max/{max}")]
        public async Task<IActionResult> GetAllProductsAsync(decimal min, decimal max)
        {
            var pr = await _productRepo.GetAllProductsAsync(min, max);

            if (pr == null)
                return NotFound("There is no Products right now");

            

            return Ok();
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateProductAsync(CreateProductDto prDto)
        {
            if (prDto == null)
                return BadRequest("Error : There is missing data for creating a Product");

            var cat = await _categoryRepo.GetByIdAsync(prDto.CategoryId);

            if (cat == null)
                return NotFound($"Error : Category With id '{prDto.CategoryId}' not found");

            var product = new Product()
            {
                Name = prDto.Name,
                Description = prDto.Description,
                Price = prDto.Price,
                StockQuantity = prDto.StockQuantity,
                CategoryId = prDto.CategoryId
            };

            await _productRepo.AddAsync(product);
            await _productRepo.SaveChangesAsync();

            return Ok(prDto);
        }
    }

}
