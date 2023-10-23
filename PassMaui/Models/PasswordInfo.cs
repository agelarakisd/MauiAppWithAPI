using System.Security;
using SQLite;

namespace PassMaui.Models;

[Table("mytable")]
public class PasswordInfo
{
    [PrimaryKey, AutoIncrement]
    public int SiteId { get; set; }

    [Column("Site")]
    public string Site { get; set; }

    [Column("Description")]
    public string Description { get; set; }

    [Column("Username")]
    public string Username { get; set; }

    [Column("Password")]
    public string Password { get; set; }
}



