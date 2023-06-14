using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace UmAlgoraa.Models
{
    

                         //pacage has ready to go tables
    public class ApplicationUser : IdentityUser
    {
        //no need for the other properties because they already exist in identity dbconext

        public string EmpName { get; set; }

        public string NationalId { get; set; }
        public string Ministry { get; set; }
        public string Manger { get; set; }
        public string Department { get; set; }

        //[Compare("PasswordHash", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }
}
