namespace KundenUmfrageTool.Api.Dtos.Users
{

    // Benutzer-Liste im Frontend
    public class UserListDto
    {
        public int Id { get; set; } 
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }


    }
}
/*
 
 Warum UserList Dto ---> FullName und Role ??           eigentlich FirstName , LastName -->UserDetailDTO

 Weil UserListDTO Nur Datei angezeigt Nicht bearbeiten , nicht ändert desahlb so ist reicht 
 
 
 
 */