namespace KundenUmfrageTool.Api.Dtos.Users
{


    //Bearbeitung Formaular
    public class UserDetailDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public int RoleId { get; set; }
        public bool IsActive { get; set; }



    }
}
