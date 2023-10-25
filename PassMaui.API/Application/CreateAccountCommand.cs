namespace PassMaui.API.Application;

public class CreateAccountCommand
{
    public string Site { get; set; }

    public string Description { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }
}