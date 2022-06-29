using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaptaBackend
{
    public class PlantDiseases
    {
        public int ID { get; set; }
    
        [Required]
        public String Treatment { get; set; }

        [ForeignKey("Plant")]
        public int PlantID { get; set; }
        public String DiseaseName { get; set; }


        [ForeignKey("DiseaseName")]
        public virtual Disease Disease { get; set; }

        public virtual Plant Plant{ get; set; }
    }
}
