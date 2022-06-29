using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.IO;
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
    [RoutePrefix("api/Posts")]
    public class PostsController : ApiController
    {
        private Context _context = new Context();

        // GET: api/Posts'
        [HttpGet,Route("AllPosts")]
        public async Task<IHttpActionResult> GetPosts(string email)
            {
            var AllPosts= await _context.Posts.ToListAsync();

            //IdentityUser Iduser = await _manager.FindAsync(User.Identity.Name, data.OldPassword);

            ApplicationUser user = await _context.IdentityUsers.FirstOrDefaultAsync(e => e.Email == email);
            List<PostDTO> Posts = new List<PostDTO>();
            foreach (var post in AllPosts)
            {
                PostDTO postDTO = new PostDTO();

                postDTO.FirstName = post.User.FirstName;
                postDTO.LastName = post.User.LastName;
                postDTO.Nationality = post.User.Nationality.Name;
                postDTO.UserImage = post.User.ProfilePic;
                postDTO.IsLiked = user.Liked_Posts.FirstOrDefault(p => p.ID == post.ID) != null;

                postDTO.CreationDate = post.CreationDate.ToString("yyyy/MM/dd HH:mm"); ;
                postDTO.NumberOfLikes = post.InteractiveUsers.Count();
                postDTO.NumberOfComments = post.Comments.Count();
                postDTO.PlantName = post.PlantTag.Name;
                postDTO.Image = post.Image;
                postDTO.Content = post.Content;
                postDTO.PostId = post.ID;

                Posts.Add(postDTO);

            }
            return Ok(Posts);
        }

        // GET: api/Posts/5
        [Authorize, Route("UserPosts")]
        public async Task<IHttpActionResult> GetUserPosts(string email)
        {
            ApplicationUser user = await _context.IdentityUsers.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                return NotFound();
            List<PostDTO> Posts = new List<PostDTO>();
            foreach (var post in user.CreatedPosts)
            {
                PostDTO postDTO = new PostDTO();
                
                postDTO.FirstName = user.FirstName;
                postDTO.LastName = user.LastName;
                postDTO.Nationality = user.Nationality.Name;
                postDTO.UserImage = user.ProfilePic;

                postDTO.CreationDate = post.CreationDate.ToString("yyyy/MM/dd HH:mm"); ;
                postDTO.PostId = post.ID;
                postDTO.NumberOfLikes = post.InteractiveUsers.Count();
                postDTO.NumberOfComments = post.Comments.Count();
                postDTO.PlantName = post.PlantTag.Name;
                postDTO.Image = post.Image;
                postDTO.Content = post.Content;
                Posts.Add(postDTO);

            }

            return Ok(Posts);
        }
     

        [ResponseType(typeof(PostDTO))]
        public async Task<IHttpActionResult> GetPost(int id)
        {
            Post post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            PostDTO postDTO = new PostDTO();
            
            postDTO.FirstName = post.User.FirstName;
            postDTO.LastName = post.User.LastName;
            postDTO.Nationality = post.User.LastName;

            postDTO.NumberOfLikes = post.InteractiveUsers.Count();
            postDTO.NumberOfComments = post.Comments.Count();
            postDTO.UserImage = post.User.ProfilePic;
            postDTO.Image = post.Image;
            postDTO.PlantName = post.PlantTag.Name;
            postDTO.PostId = post.ID;

            return Ok(postDTO);
        }

        // PUT: api/Posts/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPost(int id, Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != post.ID)
            {
                return BadRequest();
            }

            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
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

        // POST: api/Posts
        [ResponseType(typeof(Post))]
        public async Task<IHttpActionResult> PostPost(string email , Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser user = await _context.IdentityUsers.FirstOrDefaultAsync(u => u.Email == email);

            if(user == null)
                return NotFound();

            post.User=user;
            _context.Posts.Add(post);
            user.CreatedPosts.Add(post);

            await _context.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = post.ID }, post);
        }

        // DELETE: api/Posts/5
        [ResponseType(typeof(Post))]
        public async Task<IHttpActionResult> DeletePost(int id , string email)
        {
            Post post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            ApplicationUser user = await _context.IdentityUsers.FirstOrDefaultAsync(u => u.Email == email);
            user.CreatedPosts.Remove(post);

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return Ok(post);
        }

        [HttpPost]
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Count(e => e.ID == id) > 0;
        }
    }
}