using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleRentalAPI.Context;
using VehicleRentalAPI.Entities;

namespace VehicleRentalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly VehicleRentalContext context;

        public RentalsController(VehicleRentalContext context)
        {
            this.context = context;
        }
        [HttpPost]
        [Route("create-rental")]
        public async Task<ActionResult<Rental>> createRental([FromBody] Rental rental)
        {
            context.Rentals.Add(rental);
            await context.SaveChangesAsync();
            return Ok(rental);
        }
        [HttpGet]
        [Route("get-rentals")]
        public async Task<ActionResult<IEnumerable<Rental>>> getallRental()
        {
            return await context.Rentals.ToListAsync();
        }
        [HttpGet]
        [Route("get-rental/{id}")]
        public async Task<ActionResult<Rental>> getOneRental(int id)
        {
            var toDisplay = await context.Rentals.FindAsync(id);
            if (toDisplay is null)
            {
                return NotFound();
            }
            return Ok(toDisplay);
        }
        [HttpPut]
        [Route("update-rental/{id}")]
        public async Task<IActionResult> updateRental(int id, [FromBody] Rental rental)
        {
            if (id != rental.Id)
            {
                return BadRequest();
            }
            if (!context.Rentals.Any(b => b.Id == id))
            {
                return NotFound();
            }
            context.Entry(rental).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete]
        [Route("delete-rental/{id}")]
        public async Task<IActionResult> deleteRental(int id)
        {
            var toDelete = await context.Rentals.FindAsync(id);
            if (toDelete is null)
            {
                return NotFound();
            }
            context.Rentals.Remove(toDelete);
            await context.SaveChangesAsync();
            return NoContent();
        }


    }
}
