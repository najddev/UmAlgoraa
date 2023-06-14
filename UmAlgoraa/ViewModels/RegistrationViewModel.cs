using System.ComponentModel.DataAnnotations;

namespace UmAlgoraa.ViewModels
{
    public class RegistrationViewModel
    {
        //[Key]
        //public int Id { get; set; }
        public string EmpName { get; set; }
        public string Email { get; set; }
        public string UserName { get ; set; }
        public string NationalId { get; set; }
        public string PhoneNumber { get; set; }
        public string Ministry { get; set; }
        public string Manger { get; set; }
        public string Department { get; set; }
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassWord { get; set; }
    }
}
