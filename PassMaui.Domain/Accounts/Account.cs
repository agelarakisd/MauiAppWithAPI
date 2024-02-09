using Newtonsoft.Json;

namespace PassMaui.Domain;

public class Account
{
    private Account(string site, string description, string username, string password)
    {
        Site = site;
        Description = description;
        Username = username;
        Password = password;
    }

    [JsonConstructor]
    private Account(int id, string site, string description, string username, string password)
    {
        Id = id;
        Site = site;
        Description = description;
        Username = username;
        Password = password;
    }

    public int Id { get;  set; }
    
    public string Site { get;  set; }

    public string Description { get;  set; }

    public string Username { get;  set; }

    public string Password { get;  set; }

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

    public void ChangeAccountDetails(string site, string description, string username, string password)
    {
        Site = site;
        Description = description;
        Username = username;
        Password = password;
    }
}