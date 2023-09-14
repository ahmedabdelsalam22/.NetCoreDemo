using System.ComponentModel.DataAnnotations;

namespace SA_Project.Models.Dtos
{
    public class LoginRequestDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
