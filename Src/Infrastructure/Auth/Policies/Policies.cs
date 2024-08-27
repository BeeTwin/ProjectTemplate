namespace Infrastructure.Auth.Policies;

public static class Policies
{
    private const string PolicySuffix = "Policy";
    public const string Default = Roles.Default + PolicySuffix;
    public const string Admin = Roles.Admin + PolicySuffix;
}
