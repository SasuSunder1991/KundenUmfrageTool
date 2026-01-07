namespace KundenUmfrageTool.Api.Dtos.Users
{
    public class UpdateUserDto
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public int RoleId { get; set; }
        public bool IsActive { get; set; }

    }
}
