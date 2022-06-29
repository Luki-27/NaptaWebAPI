using NaptaBackend;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace NaptaWebAPI.Controllers
{
    [RoutePrefix("api/Plans")]
    public class PLansController : ApiController
    {
        Context _context = new Context();

        [Route("GetAll")]
        public async Task<IHttpActionResult> GetPlans(int Id)//plant id
        {
            //var plan = await _context.Plans.Where(p => p.PlantID == Id).ToListAsync();
            var plant = await _context.Plants.FirstOrDefaultAsync(p => p.ID == Id);

            if(plant == null)
                return NotFound();

            return Ok(plant.Plans);
        }
        
        public async Task<IHttpActionResult> PostPlan(Plan plan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var plant = await _context.Plants.FirstOrDefaultAsync(p => p.ID == plan.PlantID);
            if (plant == null)
                return NotFound();

            _context.Plans.Add(plan);
            plant.Plans.Add(plan);
            await _context.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = plan.ID }, plan);
        }
    }
}
