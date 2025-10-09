using ECommerce_Case_Study.Data.Models;
using ECommerce_Case_Study.DTOs;
using ECommerce_Case_Study.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce_Case_Study.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepo _orderRepo;
        private readonly IProductRepo _productRepo;
        public OrderController(IOrderRepo orderRepo, IProductRepo productRepo)
        {
            _orderRepo = orderRepo;
            _productRepo = productRepo;
        }


        [HttpGet("customer-id/{id}")]
        public async Task<IActionResult> GetAllOrdersByCustomerIdAsync(int id)
        {
            var ord = await _orderRepo.GetAllOrdersByCustomerIdAsync(id);

            if (ord == null)
                return NotFound("Error : there aren't any ord right now");


            var orders = ord.GroupBy(x => x.Customer)
                .Select(o => new
                {
                    Customer_id = o.Key.Id,
                    Customer = o.Key.Name,
                    Total_Spending = o.Sum(s => s.TotalPrice),
                    Orders = o.Select(v => new
                    {
                        OrderId = v.Id,
                        Total_Price = v.TotalPrice,
                        Number_of_products = v.Products.Count(),
                        products = v.Products.Select(p => new
                        {
                            p.Id,
                            p.Name,
                            p.Price
                        }).ToList()
                    }).ToList()
                }).ToList();

            return Ok(orders);
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateOrderAsync(CreateOrderDto orderDto)
        {
            if (orderDto == null)
                return BadRequest("Error : There is missing data");
            

            var Allproducts = await _productRepo.GetAllAsync();

            var existingProcuts = Allproducts.Where(p => orderDto.Products.Contains(p.Id)).ToList();
            

            var order = new Order()
            {
                OrderId = orderDto.OrderId,
                TotalPrice = orderDto.TotalPrice,
                DateOfOrder = orderDto.DateOfOrder,
                CustomerId = orderDto.CustomerId,
                Products = existingProcuts
            };
            
            await _orderRepo.AddAsync(order);
            await _orderRepo.SaveChangesAsync();

            return Ok(orderDto);
        }


        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateOrderAsync(int id, CreateOrderDto orderDto)
        {
            var order = await _orderRepo.GetByIdAsync(id);

            if (order == null)
                return NotFound($"Error : Order With Id '{id}' not found!");

            if (orderDto == null)
                return BadRequest("Error : There is missing data");

            var Allproducts = await _productRepo.GetAllAsync();

            var existingProcuts = Allproducts.Where(p => orderDto.Products.Contains(p.Id)).ToList();

            order.Products.Clear();

            order.OrderId = orderDto.OrderId;
            order.TotalPrice = orderDto.TotalPrice;
            order.DateOfOrder = orderDto.DateOfOrder;
            order.CustomerId = orderDto.CustomerId;
            order.Products = existingProcuts;

            

            _orderRepo.Update(order);
            await _orderRepo.SaveChangesAsync();

            return Ok(orderDto);
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteOrderAsync(int id)
        {
            var order = await _orderRepo.GetByIdAsync(id);

            if (order == null)
                return NotFound($"Error : Order With Id '{id}' not found!");

            _orderRepo.Remove(order);
            await _orderRepo.SaveChangesAsync();
            return NoContent();
        }
    }
}
