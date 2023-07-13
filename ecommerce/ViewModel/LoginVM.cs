using System.ComponentModel.DataAnnotations;

namespace ecommerce.ViewModel
{
    public class LoginVM
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }


}
