using System.ComponentModel.DataAnnotations;

namespace MatrimonyAPI.Models
{
    public class RegisterUserModel
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Email { get; set; }

        public string? MobileNo { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
