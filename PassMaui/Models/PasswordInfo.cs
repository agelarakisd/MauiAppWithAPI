using System.Security;

namespace PassMaui.Models
{
    public record PasswordInfo(
        string Site,
        string Description,
        string Username,
        Guid UserId,
        string Password );
}
