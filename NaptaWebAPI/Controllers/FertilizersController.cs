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
    public class FertilizersController : ApiController
    {
        private Context _context = new Context();

        // GET: api/Fertilizers
        public IQueryable<Fertilizer> GetFertilizers()
        {
            return _context.Fertilizers;
        }

        // GET: api/Fertilizers/5
        [ResponseType(typeof(Fertilizer))]
        public async Task<IHttpActionResult> GetFertilizer(int id)
        {
            Fertilizer fertilizer = await _context.Fertilizers.FindAsync(id);
            if (fertilizer == null)
            {
                return NotFound();
            }

            return Ok(fertilizer);
        }

        // PUT: api/Fertilizers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFertilizer(int id, Fertilizer fertilizer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fertilizer.ID)
            {
                return BadRequest();
            }

            _context.Entry(fertilizer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FertilizerExists(id))
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

        // POST: api/Fertilizers
        [ResponseType(typeof(Fertilizer))]
        public async Task<IHttpActionResult> PostFertilizer(Fertilizer fertilizer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Fertilizers.Add(fertilizer);
            await _context.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = fertilizer.ID }, fertilizer);
        }

        // DELETE: api/Fertilizers/5
        [ResponseType(typeof(Fertilizer))]
        public async Task<IHttpActionResult> DeleteFertilizer(int id)
        {
            Fertilizer fertilizer = await _context.Fertilizers.FindAsync(id);
            if (fertilizer == null)
            {
                return NotFound();
            }

            _context.Fertilizers.Remove(fertilizer);
            await _context.SaveChangesAsync();

            return Ok(fertilizer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FertilizerExists(int id)
        {
            return _context.Fertilizers.Count(e => e.ID == id) > 0;
        }
    }
}