using Newtonsoft.Json;

namespace PassMaui.Domain;

public class Account
{
    [JsonConstructor]
    private Account(string site, string description, string username, string password)
    {
        Site = site;
        Description = description;
        Username = username;
        Password = password;
    }

    public int Id { get; private set; }
    
    public string Site { get; private set; }

    public string Description { get; private set; }

    public string Username { get; private set; }

    public string Password { get; private set; }

    public static Account Create(
        string site,
        string description,
        string username,
        string password)
    {
        return new Account(site, description, username, password);
    }

    public void ChangePassword(string password)
    {
        Password = password;
    }
}