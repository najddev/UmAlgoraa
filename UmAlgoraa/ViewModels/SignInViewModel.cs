using System.ComponentModel.DataAnnotations;

namespace UmAlgoraa.ViewModels
{

    public class SignInViewModel
    { 
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool rememberMe { get; set; }
    }

}
