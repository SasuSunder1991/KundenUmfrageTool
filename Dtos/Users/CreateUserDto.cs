namespace KundenUmfrageTool.Api.Dtos.Users
{
    // benutzer anlegen
    public class CreateUserDto
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public int RoleId { get; set; } // 1:QM , 2= RM
        public string Password { get; set; } = "";


    }
}
