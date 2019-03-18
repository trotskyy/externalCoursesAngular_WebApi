using System.ComponentModel.DataAnnotations;

namespace AwesomeLists.Services.Auth
{
    public sealed class SignUpModel
    {
        [Required]
        public SignInModel SignInModel { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
