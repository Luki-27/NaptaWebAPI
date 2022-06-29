using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaptaWebAPI.Models
{
    public class PlanFertilizerDTO
    {
        public int WeekNum { get; set; }

        public List<FertWithQuntity> FertWithQuntities { get; set; }= new List<FertWithQuntity>();
     
    }

    public class FertWithQuntity
    {
        public double Quantity { get; set; }

        public string FertilizerName { get; set; }
    }
}