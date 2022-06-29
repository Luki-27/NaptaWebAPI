using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NaptaWebAPI.Models
{
    public class PostDTO
    {
        public int PostId { get; set; }
        public string Image { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Nationality { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string CreationDate { get; set; }
        public bool IsLiked { get; set; }

        [Required]
        public int NumberOfComments { get; set; }
        [Required]
        public int NumberOfLikes{ get; set; }
        public string UserImage { get; set; }
        public string PlantName { get; set; }
       
        
        
    }
}