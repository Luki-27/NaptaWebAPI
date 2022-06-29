using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaptaBackend
{
    [Table("Test")]
    public class Test
    {
        public int ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [Required]
        public String Image { get; set; }
        

        [ForeignKey("Plant")]
        public int PlantID { get; set; }
        public bool IsHealthy { get; set; }
        
        public virtual ICollection<TestDisease> TestDiseases { get; set; }
        
        public virtual ApplicationUser User { get; set; }
        public virtual Plant Plant{ get; set; }

    }

}
