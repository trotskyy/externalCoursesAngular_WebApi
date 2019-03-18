using System.ComponentModel.DataAnnotations;

namespace AwesomeLists.Services.Auth
{
    public sealed class SignInModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
