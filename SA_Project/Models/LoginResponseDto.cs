namespace SA_Project.Models
{
    public class LoginResponseDto
    {
        public ApplicationUser User { get; set; }
        public string Token { get; set; }
    }
}
