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
    [Table("Fertilizer")]

    public class Fertilizer
    {
        public int ID { get; set; }

        [Required]
        public String Name { get; set; }


        [JsonIgnore]
        public virtual ICollection<PlanFertilizer> PlanFertilizers { get; set; }
    }
}
