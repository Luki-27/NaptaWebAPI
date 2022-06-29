using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NaptaBackend;
using NaptaWebAPI.Models;

namespace NaptaWebAPI.Controllers
{
    [RoutePrefix("api/users")]
    public class ApplicationUsersController : ApiController
    {
        private Context _context = new Context();

        [Route("ALL")]
        [HttpGet, ResponseType(typeof(List<UserDTO>))]
        public async Task<IHttpActionResult> GetApplicationUsers()
        {
            var appUsers = await _context.IdentityUsers.ToListAsync();

            if (appUsers == null)
            {
                return NotFound();
            }
            List<UserDTO> usersDTO = new List<UserDTO>();

            foreach (var user in appUsers)
            {
                usersDTO.Add
                    ( 
                    new UserDTO
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        NationalityName = user.Nationality?.Name ??null ,
                        ProfilePic = user.ProfilePic
                    }
                    );
                Console.WriteLine(user.Email);
            }

            return Ok(usersDTO);
        }

        [Route("getdata")]
        [Authorize, HttpGet, ResponseType(typeof(UserDTO))]
        public async Task<IHttpActionResult> GetApplicationUser(string email)
        {
            ApplicationUser appUser = await _context.IdentityUsers.FirstOrDefaultAsync(e => e.Email == email);

            if (appUser == null)
            {
                return NotFound();
            }

            UserDTO userDTO = new UserDTO()
            {
                Email = email,
                FirstName = appUser.FirstName,
                LastName = appUser.LastName,
                PhoneNumber = appUser.PhoneNumber,
                NationalityName = appUser.Nationality.Name,
                ProfilePic = appUser.ProfilePic
            };

            return Ok(userDTO);
        }

        
        [Route("Update")]
        [Authorize, ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutApplicationUser(UserDTO userDTO)
        {
            if (!ModelState.IsValid)    
            {
                return BadRequest(ModelState);
            }


            ApplicationUser user = await _context.IdentityUsers.FirstOrDefaultAsync(e => e.Email == userDTO.Email);//User.Identity.Name);

            if (user == null)
                return BadRequest("Not valid Email");

            user.FirstName = userDTO.FirstName;
            user.LastName = userDTO.LastName;
            user.PhoneNumber = userDTO.PhoneNumber;
            user.ProfilePic = userDTO.ProfilePic;
            //user.Nationality.Name = userDTO.NationalityName;//new Nationality { Name = userDTO.NationalityName };

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Updated Successfully");
        }        

    }
}