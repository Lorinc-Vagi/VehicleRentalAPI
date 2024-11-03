using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleRentalAPI.Context;
using VehicleRentalAPI.Entities;

namespace VehicleRentalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehliclesController : ControllerBase
    {
        private readonly VehicleRentalContext context;

        public VehliclesController(VehicleRentalContext context)
        {
            this.context = context;
        }
        [HttpPost]
        [Route("create-vehicle")]
        public async Task<ActionResult<Vehicle>> create([FromBody] Vehicle vehicle)
        {
            context.Vehicles.Add(vehicle);
            await context.SaveChangesAsync();
            return Ok(vehicle);
        }
        [HttpGet]
        [Route("get-vehicles")]
        public async Task<ActionResult<IEnumerable<Vehicle>>> getall()
        {
            return await context.Vehicles.ToListAsync();
        }
        [HttpGet]
        [Route("get-vehicle/{id}")]
        public async Task<ActionResult<Vehicle>> getOne(int id)
        {
            var toDisplay = await context.Vehicles.FindAsync(id);
            if (toDisplay is null)
            {
                return NotFound();
            }
            return Ok(toDisplay);
        }
        [HttpPut]
        [Route("update-vehicle/{id}")]
        public async Task<IActionResult> update(int id, [FromBody] Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return BadRequest();
            }
            if (!context.Vehicles.Any(b => b.Id == id))
            {
                return NotFound();
            }
            context.Entry(vehicle).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete]
        [Route("delete-vehicle/{id}")]
        public async Task<IActionResult> delete(int id)
        {
            var toDelete = await context.Vehicles.FindAsync(id);
            if (toDelete is null)
            {
                return NotFound();
            }
            context.Vehicles.Remove(toDelete);
            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}
