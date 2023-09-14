namespace SA_Project.Models.Dtos
{
    public class LoginResponseDto
    {
        public ApplicationUser User { get; set; }
        public string Token { get; set; }
    }
}
