namespace Template.Endpoints.Admin.Auth.Roles.Responses;

public sealed record RolesResponse(IEnumerable<RoleDto> Roles);
public sealed record RoleDto(Guid Id, string Name);
