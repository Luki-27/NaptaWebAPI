using NaptaBackend;
using NaptaWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


using System.Threading.Tasks;
using System.Web.Http;

namespace NaptaWebAPI.Controllers
{
    [RoutePrefix("api/UsersComments")]

    public class PostsCommentsController : ApiController
    {
        Context _context = new Context();

        [Authorize, Route("Comments")]
        public async Task<IHttpActionResult> GetPostComments(int id)
        {
            Post post = await _context.Posts.FirstOrDefaultAsync(p => p.ID == id);

            if (post == null)
                return NotFound();

            var postComments = new List<CommentDTO>();
            foreach (var item in post.Comments)
            {
                postComments.Add(
                    new CommentDTO
                    {
                        CommentID = item.ID,
                        Content = item.Content,
                        FirstName = item.User.FirstName,
                        LastName = item.User.LastName,
                        PostID = item.PostID,
                        UserImage = item.User.ProfilePic
                    }
                    );
            }
            
            return Ok(postComments);
        }

        [HttpPost, Authorize, Route("Add")]
        public async Task<IHttpActionResult> AddCommentToPost(Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Post post = await _context.Posts.FirstOrDefaultAsync(u => u.ID == comment.PostID);
            ApplicationUser user = await _context.IdentityUsers.FirstOrDefaultAsync(u => u.Email == comment.Email);

            if (post == null || user == null)
                return NotFound();

            try
            {
                user.Comments.Add(comment);
                post.Comments.Add(comment);

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
        public async Task<IHttpActionResult> DeleteCommentFromPost(Comment comment)
        {
            
            Post post = await _context.Posts.FirstOrDefaultAsync(u => u.ID == comment.PostID);
            ApplicationUser user = await _context.IdentityUsers.FirstOrDefaultAsync(u => u.Email == comment.Email);

            if (post == null || !post.Comments.Contains(comment) || user == null)
            {
                return NotFound();
            }
            
            try
            {
                comment = post.Comments.FirstOrDefault(c => c.ID == comment.ID);

                post.Comments.Remove(comment);
                user.Comments.Remove(comment);
                await _context.SaveChangesAsync();
                return Ok("Removed Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }

}
