namespace resevation_be.DTOs
{
    public class UserDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? IdToken { get; set; }
        public bool IsLoggedIn { get; set; }
    }
}