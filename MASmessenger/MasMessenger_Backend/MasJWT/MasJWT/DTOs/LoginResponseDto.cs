namespace MasJWT.DTOs
{
    public class LoginResponseDto
    {
        public int Oid { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public string Token { get; set; }
    }
}