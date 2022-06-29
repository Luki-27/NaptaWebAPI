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

    [RoutePrefix("api/UsersLikes")]

    public class PostsLikesController : ApiController
    {

        Context _context = new Context();

        [Authorize, Route("Likes")]
        public async Task<IHttpActionResult> GetPostLikes(int id)
        {
            
            Post post = await _context.Posts.FirstOrDefaultAsync(p=> p.ID == id);

            if (post == null)
                return NotFound();
            return Ok(post.InteractiveUsers);
        }

        [HttpPost, Authorize, Route("Like")]
        public async Task<IHttpActionResult> LikeAPost(string email ,int id)
        {
            Post post = await _context.Posts.FirstOrDefaultAsync(p => p.ID == id);
            ApplicationUser user = await _context.IdentityUsers.FirstOrDefaultAsync(u => u.Email == email);


            if (post == null||user==null )
                return NotFound();

          
            if (!user.Liked_Posts.Contains(post))
            {
                post.InteractiveUsers.Add(user);
                user.Liked_Posts.Add(post);

            }
            else
            {
                return BadRequest("You already like the post ");
            }

            try
            {
                await _context.SaveChangesAsync();
                //created with location
                return Ok("Liked Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete, Authorize, Route("Dislike")]
        public async Task<IHttpActionResult> DisLike(string email, int id)
        {
            ApplicationUser user = await _context.IdentityUsers.FirstOrDefaultAsync(u => u.Email == email);
            Post post = await _context.Posts.FirstOrDefaultAsync(p => p.ID == id);
            if (user == null || post == null)
                return NotFound();


            if (user.Liked_Posts.Contains(post))
            {
                user.Liked_Posts.Remove(post);
                post.InteractiveUsers.Remove(user);
            }
            else
            {
                return BadRequest("You did not Like this post");
            }

            try
            {
                await _context.SaveChangesAsync();
                return Ok("Dislike Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
