namespace FastDrink.Application.Users.DTOs;

public class ChangePasswordRequest
{
    public string Password { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}
