using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaptaBackend
{
    [Table("ApplicationUser")]
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public override string Email { get; set; }

        public string ProfilePic { get; set; }

        public virtual Nationality Nationality { get; set; }// = new Nationality();


        [InverseProperty("User")]
        [JsonIgnore]

        public virtual ICollection<Post> CreatedPosts { get; set; }


        [InverseProperty("InteractiveUsers")]
        [JsonIgnore]

        public virtual ICollection<Post> Liked_Posts { get; set; }
        [JsonIgnore]
       
        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Plant> InterestingPlants { get; set; }
        public virtual ICollection<Test> Tests { get; set; }

    }
}
