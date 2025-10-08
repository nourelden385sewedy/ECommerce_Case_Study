using ECommerce_Case_Study.Data.Models;
using ECommerce_Case_Study.DTOs;
using ECommerce_Case_Study.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce_Case_Study.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepo _customerRepo;
        public CustomerController(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCustomerAsync(CreateCustomerDto cusDto)
        {
            if (cusDto == null)
                return BadRequest("Error : There is missing data!");

            if (cusDto.Password != cusDto.ConfirmPassword)
                return BadRequest("Error : Password and Confirm Password should be match");

            var customer = new Customer()
            {
                Name = cusDto.Name,
                Contact = cusDto.Contact,
                Email = cusDto.Email,
                Password = cusDto.Password,
                ConfirmPassword = cusDto.ConfirmPassword,
                CustomerProfile = new CustomerProfile
                {
                    Address = cusDto.ProfileDto.Address ?? "Default Address",
                    DateOfBirth = cusDto.ProfileDto.DateOfBirth
                }
            };

            await _customerRepo.AddAsync(customer);
            await _customerRepo.SaveChangesAsync();
            return Ok(new {message = "Customer Created Successfully", cusDto});
        }
    }
}
