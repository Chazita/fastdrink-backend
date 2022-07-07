namespace FastDrink.Application.Users.DTOs;

public class ChangeNameRequest
{
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Password { get; set; } = "";
}
