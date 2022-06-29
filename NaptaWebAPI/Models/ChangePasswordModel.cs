using System.ComponentModel.DataAnnotations;

namespace NaptaWebAPI.Models
{
    public class ChangePasswordModel
    {
        [Required]
        public string OldPassword { get; set; }
        
        [Required]
        public string NewPassword { get; set; } 

        [Required,Compare("NewPassword")]
        public string ConfirmPassword { get; set; } 
        
    }
}