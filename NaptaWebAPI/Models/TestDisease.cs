using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaptaBackend
{
    [Table("TestDisease")]
    public class TestDisease
    {
        public int ID { get; set; }
        public int TestID { get; set; }
        public String DiseaseName { get; set; }


        [ForeignKey("TestID")]
        public virtual Test Test{ get; set; }


        [ForeignKey("DiseaseName")]
        public virtual Disease Disease{ get; set; }
    }
}
