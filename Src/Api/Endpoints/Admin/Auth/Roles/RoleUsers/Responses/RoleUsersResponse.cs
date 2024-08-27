namespace Template.Endpoints.Admin.Auth.Roles.RoleUsers.Responses;

public sealed record RoleUsersResponse(int TotalCount, IEnumerable<RoleUserDto> Users);
public sealed record RoleUserDto(Guid Id, string Username);
