namespace KundenUmfrageTool.Api.Dtos.Auth
{
    public record RegisterDto(string Email, string Password, string FirstName, string LastName, int RoleId);    //Imutable
    public record LoginDto(string Email, string Password);
    public record AuthResponse(string Token);
}


/*
  
  Was ist DTO?
  DTO = DAta Transfer Objekt
- Ein Objekt, das NUR die Daten enthält, die das Frontend braucht.
- Es ist NICHT die Entity (also nicht die echte Datenbank-Tabelle).
   
  5 Gründe
  1) Sicherheit:
   Das Frontend sieht nur das, was es sehen darf.
 - PasswortHash, interne IDs, Beziehungen bleiben verborgen.

2) Saubere API
  Backend und Frontend sind getrennt 
  Das Frontend bekommt , saubere Schön Daten

3) Keine Abhängigkeit Von der Datenbankstruktur
  Wenn du DB änderst --> FRontend bricht nicht

4) Optimierte Datenmenge:
  API gibt nur das zurück, Was wirklich gebraucht wird.

5)Klare Rollen-Trennung
Entities = Datenbank Objekt
DTOS = API Antworten für Angular

  
  Warum benutze ich "record" für meine Auth-DTOs?

  -  DTOs sind reine Datencontainer (Data Transfer Objects).
  -  Sie enthalten keine Logik und werden nicht verändert – nur eingelesen oder zurückgegeben.
  -  records sind dafür ideal, weil:

     1) Immutable by default (Daten werden nicht ungewollt verändert)
     2) Weniger Code → übersichtlich und schnell zu lesen
     3) Perfekt geeignet für Requests/Responses in Web-APIs
     4) Primary Constructor macht den Code kurz, eindeutig und sicher
     5) records werden automatisch wertbasiert verglichen (nicht referenzbasiert)


  "record" ist die modernere und sauberere Wahl für kleine, feste Datentransfer-Objekte 
  wie RegisterDto, LoginDto und AuthResponse.

  Wann würde ich class verwenden?
  --------------------------------
  - Wenn das Objekt komplexer ist
  -  Wenn es veränderbar sein soll (mutable)
  - Wenn es Methoden, Logik oder interne Zustände hat
  - Wenn Navigation zwischen Entities nötig ist (z. B. Benutzer, Restaurant etc.)

  -----------------------------
  DTOs (Request/Response) → record
  Geschäftsobjekte / Entities → class

  Dadurch bleibt unser Code sauber, modern und gut strukturiert.
*/
