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
    [Table("Plan")]
    public class Plan
    {
        public int ID { get; set; }

        [Required]
        [ForeignKey("Plant")]
        public int PlantID { get; set; }
        
        [JsonIgnore]
        public virtual Plant Plant{ get; set; }

        [JsonIgnore]
        public virtual ICollection<PlanFertilizer> PlanFertilizers { get; set; }

    }
}
