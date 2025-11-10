namespace KundenUmfrageTool.Api.Dtos
{
    public record RegisterDto(string Email, string Password, string FirstName, string LastName, int RoleId);
    public record LoginDto(string Email, string Password);
    public record AuthResponse(string Token);
}
