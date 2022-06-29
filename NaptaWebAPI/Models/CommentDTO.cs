using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaptaWebAPI.Models
{
    public class CommentDTO
    {
        public int CommentID { get; set; }
        public string Content { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public String UserImage { get; set; }
        public int PostID { get; set; }
    }
}