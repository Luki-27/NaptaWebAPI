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

    [RoutePrefix("api/UserPlants")]
    public class InterestedPlantsController : ApiController
    {

        Context _context = new Context();

        [Authorize, Route("Plants")]

        public async Task<IHttpActionResult> GetUserPlants(string email)
        {
            ApplicationUser user = await _context.IdentityUsers.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return NotFound();

            
            return Ok(user.InterestingPlants);
        }

        
        
        [HttpPost, Authorize, Route("Add")]
        public async Task<IHttpActionResult> AddPlantToUser(string email,Plant plant)//plant must has id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ApplicationUser user = await _context.IdentityUsers.FirstOrDefaultAsync(u => u.UserName == email);
            plant = await _context.Plants.FindAsync(plant.ID);

            if (user == null || plant == null)
                return NotFound();


            if (user.InterestingPlants.Count(p => p.ID == plant.ID) == 0)
            {
                plant.InterestedUsers.Add(user);
                user.InterestingPlants.Add(plant);
            }
            else
            {
                return BadRequest("Plant is Invalid or is Already Added");
            }

            try
            {
                await _context.SaveChangesAsync();
                //created with location
                return Ok("Added Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete, Authorize, Route("Delete")]
        public async Task<IHttpActionResult> DeletePlantFromUser(string email, Plant plant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ApplicationUser user = await _context.IdentityUsers.FirstOrDefaultAsync(u => u.Email == email);
            plant = await _context.Plants.FirstOrDefaultAsync(p => p.ID == plant.ID);
            if (user == null || plant == null)
                return NotFound();


            if (user.InterestingPlants.Count(p => p.ID == plant.ID) != 0)
            {
                plant.InterestedUsers.Remove(user);
                user.InterestingPlants.Remove(plant);
            }
            else
            {
                return BadRequest("Plant is Invalid or already is not  Exist");
            }

            try
            {
                await _context.SaveChangesAsync();
                return Ok("Removed Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost, Authorize, Route("AddList")]
        public async Task<IHttpActionResult> AddListPlantToUser(string email, List<Plant>plants)//plant must has id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser user = await _context.IdentityUsers.FirstOrDefaultAsync(u => u.UserName == email);
            if (user == null)
                return NotFound();

            user.InterestingPlants.Clear();

            foreach (var plant in plants)
            {
                var pl = await _context.Plants.FirstOrDefaultAsync(p => p.ID == plant.ID);
                if (pl == null)
                    return NotFound();

                user.InterestingPlants.Add(pl) ;
                pl.InterestedUsers.Add(user);
            }


            try
            {
                await _context.SaveChangesAsync();
                //created with location
                return Ok("Added Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
