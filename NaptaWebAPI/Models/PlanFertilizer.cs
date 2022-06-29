using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NaptaBackend
{
    public class PlanFertilizer
    {
        public int Id { get; set; }
        public int PlanID { get; set; }

        [Required]
        public int WeekNum { get; set; }

        [Required]
        public double Quantity { get; set; }

        [ForeignKey("Fertilizer")]
        public int FertilizerID { get; set; }

        [JsonIgnore]
        public virtual Fertilizer Fertilizer { get; set; }
        
        [JsonIgnore]
        public virtual Plan Plan { get; set; }

    }
}