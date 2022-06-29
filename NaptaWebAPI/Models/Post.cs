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
    [Table("Post")]
    public class Post
    {
        public int ID { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreationDate { get; set; }= DateTime.Now;

        public string Image { get; set; }

        [ForeignKey("PlantTag")]
        [Required]
        public int PlantID { get; set; }


        [InverseProperty("CreatedPosts")]
        [JsonIgnore]
        public virtual ApplicationUser User { get; set; }


        [InverseProperty("Liked_Posts")]
        [JsonIgnore]
        public virtual ICollection<ApplicationUser> InteractiveUsers { get; set; }

        [JsonIgnore]
        public virtual Plant PlantTag { get; set; }

        [JsonIgnore]
        public virtual ICollection<Comment>Comments { get; set; }

    }
}
