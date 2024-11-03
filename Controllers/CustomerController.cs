using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleRentalAPI.Context;
using VehicleRentalAPI.Entities;

namespace VehicleRentalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly VehicleRentalContext context;

        public CustomerController(VehicleRentalContext context)
        {
            this.context = context;
        }
        [HttpPost]
        [Route("create-customer")]
        public async Task<ActionResult<Customer>> createCustomer([FromBody] Customer customer)
        {
            context.Customers.Add(customer);
            await context.SaveChangesAsync();
            return Ok(customer);
        }
        [HttpGet]
        [Route("get-customers")]
        public async Task<ActionResult<IEnumerable<Customer>>> getallCustomer()
        {
            return await context.Customers.ToListAsync();
        }
        [HttpGet]
        [Route("get-customer/{id}")]
        public async Task<ActionResult<Customer>> getOneCustomer(int id)
        {
            var toDisplay = await context.Customers.FindAsync(id);
            if (toDisplay is null)
            {
                return NotFound();
            }
            return Ok(toDisplay);
        }
        [HttpPut]
        [Route("update-customer/{id}")]
        public async Task<IActionResult> updateCustomer(int id, [FromBody]Customer customer)
        {
            if (id!=customer.Id)
            {
                return BadRequest();
            }
            if (!context.Customers.Any(b=>b.Id==id))
            {
                return NotFound();
            }
            context.Entry(customer).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete]
        [Route("delete-customer/{id}")]
        public async Task<IActionResult> deleteCustomer(int id)
        {
            var toDelete = await context.Customers.FindAsync(id);
            if (toDelete is null)
            {
                return NotFound();
            }
            context.Customers.Remove(toDelete);
            await context.SaveChangesAsync();
            return NoContent();
        }


    }
}
