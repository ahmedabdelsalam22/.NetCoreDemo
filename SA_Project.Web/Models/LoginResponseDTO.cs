namespace SA_Project.Web.Models
{
    public class LoginResponseDTO
    {
        public UserDTO? userDTO { get; set; }
        public string Token { get; set; }
    }
}
