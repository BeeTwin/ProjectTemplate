using Infrastructure.Auth;
using Riok.Mapperly.Abstractions;
using Template.Endpoints.Admin.Auth.Roles.RoleUsers.Responses;

namespace Template.Endpoints.Admin.Auth.Roles.RoleUsers.Mapping;

[Mapper]
public static partial class AuthUserToRoleUserDtoMapping
{
    public static partial IList<RoleUserDto> ToRoleUserDtos(this IList<AuthUser> users);
}
