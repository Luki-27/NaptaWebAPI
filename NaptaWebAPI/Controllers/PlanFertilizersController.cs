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
using NaptaWebAPI.Models;

namespace NaptaWebAPI.Controllers
{
   // [RoutePrefix("api/PlansDescription")]
    public class PlanFertilizersController : ApiController
    {
        private Context _context = new Context();

        
        //[Route("FertPlans")]
        public async Task<IHttpActionResult> GetPlanFertilizer(int Id)//plan id
        {
            
            var plan = await _context.PlanFertilizers.Where(pf=> pf.PlanID == Id).ToListAsync();
            if (plan == null)
            {
                return NotFound();
            }

            List<PlanFertilizerDTO> list = new List<PlanFertilizerDTO>();

            foreach (var item in plan)
            {

                if (list.Count == 0 || list.Last().WeekNum != item.WeekNum)
                {
                    PlanFertilizerDTO dto = new PlanFertilizerDTO();
                    dto.WeekNum = item.WeekNum;
                    dto.FertWithQuntities.
                        Add(
                        new FertWithQuntity
                        {
                            FertilizerName = item.Fertilizer.Name,
                            Quantity = item.Quantity,
                        });
                    list.Add(dto);
                }
                else
                {
                    list.Last().FertWithQuntities.
                        Add(
                        new FertWithQuntity
                        {
                            FertilizerName = item.Fertilizer.Name,
                            Quantity = item.Quantity,
                        });
                }
            }

            return Ok(list);
        }


        // GET: api/PlanFertilizers
        public IQueryable<PlanFertilizer> GetPlanFertilizers()
        {
            return _context.PlanFertilizers;
        }

        // PUT: api/PlanFertilizers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPlanFertilizer(int id, PlanFertilizer planFertilizer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != planFertilizer.Id)
            {
                return BadRequest();
            }

            _context.Entry(planFertilizer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanFertilizerExists(id))
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

        // POST: api/PlanFertilizers
        [ResponseType(typeof(PlanFertilizer))]//,Route("Add")]
        public async Task<IHttpActionResult> PostPlanFertilizer(PlanFertilizer planFertilizer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var plan = _context.Plans.FirstOrDefault(p => p.ID == planFertilizer.PlanID);
            if (plan == null)
                return BadRequest("PLAN ISN'T EXIST");
            _context.PlanFertilizers.Add(planFertilizer);
            await _context.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = planFertilizer.Id }, planFertilizer);
        }

        // DELETE: api/PlanFertilizers/5
        [ResponseType(typeof(PlanFertilizer))]
        public async Task<IHttpActionResult> DeletePlanFertilizer(int id)
        {
            PlanFertilizer planFertilizer = await _context.PlanFertilizers.FindAsync(id);
            if (planFertilizer == null)
            {
                return NotFound();
            }

            _context.PlanFertilizers.Remove(planFertilizer);
            await _context.SaveChangesAsync();

            return Ok(planFertilizer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlanFertilizerExists(int id)
        {
            return _context.PlanFertilizers.Count(e => e.Id == id) > 0;
        }
    }
}