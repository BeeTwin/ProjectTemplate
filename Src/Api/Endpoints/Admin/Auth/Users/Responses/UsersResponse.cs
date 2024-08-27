namespace Template.Endpoints.Admin.Auth.Users.Responses;

public sealed record UsersResponse(int TotalCount, IEnumerable<UserDto> Users);
public sealed record UserDto(Guid Id, string Username);
