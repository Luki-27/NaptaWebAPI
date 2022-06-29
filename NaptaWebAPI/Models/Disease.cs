using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaptaBackend
{
    [Table("Disease")]
    public class Disease
    {
        [Key]
        public String Name { get; set; }

        [Required]
        public String Description { get; set; }

        public String Treatment { get; set; }
        public virtual ICollection<PlantDiseases> PlantDiseases { get; set; }
        public virtual ICollection<TestDisease> TestDiseases { get; set; }

    }
}
