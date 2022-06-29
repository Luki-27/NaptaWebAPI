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
    [Table("Plant")]
    public class Plant
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
        
        public string ImagePath { get; set; }
        [JsonIgnore]
        public virtual ICollection<ApplicationUser> InterestedUsers { get; set; }

        [JsonIgnore]
        public virtual ICollection<Plan> Plans{ get; set; }
        
        [JsonIgnore]
        public virtual ICollection<Test> Tests{ get; set; }
        
        [JsonIgnore]
        public virtual ICollection<Disease> Diseases{ get; set; }


    }
}
