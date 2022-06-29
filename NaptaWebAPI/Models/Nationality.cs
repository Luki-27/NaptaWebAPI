using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NaptaBackend
{
    public class Nationality
    {

        [Required,Key]
        public string Name { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}