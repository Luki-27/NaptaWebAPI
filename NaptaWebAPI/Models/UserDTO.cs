using NaptaBackend;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NaptaWebAPI.Models
{
    public class UserDTO
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required,EmailAddress]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string ProfilePic { get; set; }

//        [Required]
        public String NationalityName { get; set; }

    }

}
