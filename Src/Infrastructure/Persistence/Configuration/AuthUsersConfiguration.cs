using Domain.Users;
using Infrastructure.Auth;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class AuthUsersConfiguration : IEntityTypeConfiguration<AuthUser>
{
    public void Configure(EntityTypeBuilder<AuthUser> builder)
    {
        builder.HasOne<User>(au => au.DomainUser)
            .WithOne()
            .HasForeignKey<AuthUser>("DomainUserId");

        builder.Navigation(au => au.DomainUser)
            .AutoInclude();
    }
}
