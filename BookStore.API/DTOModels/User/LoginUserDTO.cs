using System.ComponentModel.DataAnnotations;

namespace BookStore.API.DTOModels.User
{
    public class LoginUserDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
