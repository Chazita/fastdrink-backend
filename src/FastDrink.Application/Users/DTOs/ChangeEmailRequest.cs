namespace FastDrink.Application.Users.DTOs;

public class ChangeEmailRequest
{
    public string NewEmail { get; set; } = "";

    public string Password { get; set; } = "";
}
