using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using NaptaBackend;

namespace NaptaWebAPI.Controllers
{
    public class PlantsController : ApiController
    {
        private Context _context = new Context();

        // GET: api/Plants
        public IQueryable<Plant> GetPlants()
        {
            return _context.Plants;
        }

        // GET: api/Plants/5
        [ResponseType(typeof(Plant))]
        public async Task<IHttpActionResult> GetPlant(int id)
        {
            Plant plant = await _context.Plants.FindAsync(id);
            if (plant == null)
            {
                return NotFound();
            }

            return Ok(plant);
        }

        // PUT: api/Plants/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPlant(int id, Plant plant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != plant.ID)
            {
                return BadRequest();
            }

            _context.Entry(plant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlantExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Plants
        [ResponseType(typeof(Plant))]
        public async Task<IHttpActionResult> PostPlant(Plant plant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Plants.Add(plant);
            await _context.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = plant.ID }, plant);
        }

        // DELETE: api/Plants/5
        [ResponseType(typeof(Plant))]
        public async Task<IHttpActionResult> DeletePlant(int id)
        {
            Plant plant = await _context.Plants.FindAsync(id);
            if (plant == null)
            {
                return NotFound();
            }

            _context.Plants.Remove(plant);
            await _context.SaveChangesAsync();

            return Ok(plant);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlantExists(int id)
        {
            return _context.Plants.Count(e => e.ID == id) > 0;
        }
    }
}