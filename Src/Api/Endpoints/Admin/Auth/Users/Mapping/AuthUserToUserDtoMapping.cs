using Infrastructure.Auth;
using Riok.Mapperly.Abstractions;
using Template.Endpoints.Admin.Auth.Users.Responses;

namespace Template.Endpoints.Admin.Auth.Users.Mapping;

[Mapper]
public static partial class AuthUserToUserDtoMapping
{
    public static partial List<UserDto> ToUserDtos(this List<AuthUser> users);
}
