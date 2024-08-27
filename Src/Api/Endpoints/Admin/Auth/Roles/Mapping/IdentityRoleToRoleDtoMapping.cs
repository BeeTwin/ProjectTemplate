using Microsoft.AspNetCore.Identity;
using Riok.Mapperly.Abstractions;
using Template.Endpoints.Admin.Auth.Roles.Responses;

namespace Template.Endpoints.Admin.Auth.Roles.Mapping;

[Mapper]
public static partial class IdentityRoleToRoleDtoMapping
{
    public static partial List<RoleDto> ToRoleDtos(this List<IdentityRole> roles);
}
