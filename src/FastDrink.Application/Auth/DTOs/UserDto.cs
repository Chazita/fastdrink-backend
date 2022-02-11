using FastDrink.Application.Common.Mappings;
using FastDrink.Domain.Entities;

namespace FastDrink.Application.Auth.DTOs;

public class UserDto : IMapFrom<User>
{
    public int Id { get; set; }

    public string Email { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }
}
