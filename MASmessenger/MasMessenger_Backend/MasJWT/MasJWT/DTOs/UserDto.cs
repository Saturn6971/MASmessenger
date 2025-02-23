namespace MasJWT.DTOs
{
    public class UserDto
    {
        public int Oid { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public byte[]? Image { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Job { get; set; }
        public string? Nationality { get; set; }
        public string? Gender { get; set; }
        public string? Role { get; set; }
    }
}