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
    [Table("Comment")]
    public class Comment
    {
        public int ID { get; set; }
        
        [Required]
        public string Content { get; set; }
        
        [Required]
        public string Email { get; set; }

        [Required, ForeignKey("Post")]
        public int PostID { get; set; }

        [JsonIgnore]
        public virtual Post Post { get; set; }
        [JsonIgnore]
        public virtual ApplicationUser User { get; set; }


    }
}
