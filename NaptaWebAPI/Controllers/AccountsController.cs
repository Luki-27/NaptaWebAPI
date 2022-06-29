using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NaptaBackend;
using NaptaWebAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace NaptaWebAPI.Controllers
{
    [RoutePrefix("api/accounts")]
    public class AccountsController : ApiController
    {
        Context _context;
        UserStore<IdentityUser> _store;
        UserManager<IdentityUser> _manager;

        public AccountsController()
        {
            _context = new Context();
            _store = new UserStore<IdentityUser>(_context);
            _manager = new UserManager<IdentityUser>(_store);
        }

        [HttpPost, Route("Register")]
        public async Task<IHttpActionResult> Register(SignUpModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ApplicationUser Iduser = MapDtoToIdUser(model);

                IdentityResult result = await _manager.CreateAsync(Iduser, model.Password);
                if (result.Succeeded)
                {
                    return Created("", $"{model.Email} is Registered Successfully");
                }

                return BadRequest(getErrors(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("ChangePassword")]
        [HttpPut, Authorize]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordModel data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            IdentityUser Iduser = await _manager.FindAsync(User.Identity.Name, data.OldPassword);

            if (Iduser == null)
                return BadRequest("Not vaild Data");

            IdentityResult result = await _manager.ChangePasswordAsync(Iduser.Id, data.OldPassword, data.NewPassword);
            if (result.Succeeded)
                return Ok("Changed Successfuly");

            return BadRequest(getErrors(result));
        }


        [HttpPut, Route("ForgetPassword")]
        public async Task<IHttpActionResult> ForgetPassword
            ([EmailAddress] string email, [FromBody] string newPassword)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            IdentityUser Iduser = await _context.IdentityUsers.FirstOrDefaultAsync(u => u.Email == email);

            if (Iduser == null)
                return BadRequest("Not vaild email");

            IdentityResult result = await _manager.RemovePasswordAsync(Iduser.Id);

            if (!result.Succeeded)
                return BadRequest(getErrors(result));

            result = await _manager.AddPasswordAsync(Iduser.Id, newPassword);

            if (result.Succeeded)
                return Ok("Changed Successfuly");

            return BadRequest(getErrors(result));

        }



        private string getErrors(IdentityResult result)
        {
            StringBuilder errorMsg = new StringBuilder();
            foreach (var item in result.Errors)
            {
                errorMsg.Append(item);
            }

            return errorMsg.ToString();
        }

        private ApplicationUser MapDtoToIdUser(SignUpModel model)
        {
            ApplicationUser Iduser = new ApplicationUser();

            Iduser.FirstName = model.FirstName;
            Iduser.LastName = model.LastName;
            Iduser.Email = model.Email;
            Iduser.Nationality = _context.Nationalities.Find(model.NationalityName);
            Iduser.PhoneNumber = model.PhoneNumber;
            Iduser.ProfilePic = model.ProfilePic;
            Iduser.UserName = model.Email;
            Iduser.PasswordHash = model.Password;

            return Iduser;
        }

    }
}
/*
  "Morocco",
  "America",
  "Brazil",
  "Canada",
  "India",
  "Mongalia",
  "USA",
  "China",
  "Russia",
  "Germany"
 */
