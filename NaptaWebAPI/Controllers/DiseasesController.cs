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
    public class DiseasesController : ApiController
    {
        private Context db = new Context();

        // GET: api/Diseases
        public IQueryable<Disease> GetDiseases()
        {
            return db.Diseases;
        }

        // GET: api/Diseases/5
        [ResponseType(typeof(Disease))]
        public async Task<IHttpActionResult> GetDisease(string id)
        {
            Disease disease = await db.Diseases.FindAsync(id);
            if (disease == null)
            {
                return NotFound();
            }

            return Ok(disease);
        }

        // PUT: api/Diseases/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDisease(string id, Disease disease)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != disease.Name)
            {
                return BadRequest();
            }

            db.Entry(disease).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiseaseExists(id))
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

        // POST: api/Diseases
        [ResponseType(typeof(Disease))]
        public async Task<IHttpActionResult> PostDisease(Disease disease)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Diseases.Add(disease);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DiseaseExists(disease.Name))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = disease.Name }, disease);
        }

        // DELETE: api/Diseases/5
        [ResponseType(typeof(Disease))]
        public async Task<IHttpActionResult> DeleteDisease(string id)
        {
            Disease disease = await db.Diseases.FindAsync(id);
            if (disease == null)
            {
                return NotFound();
            }

            db.Diseases.Remove(disease);
            await db.SaveChangesAsync();

            return Ok(disease);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DiseaseExists(string id)
        {
            return db.Diseases.Count(e => e.Name == id) > 0;
        }
    }
}