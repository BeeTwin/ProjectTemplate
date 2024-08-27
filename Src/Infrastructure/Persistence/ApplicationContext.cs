using Domain.Users;

namespace Infrastructure.Persistence;

public sealed class ApplicationContext(DbContextOptions<ApplicationContext> options) : IdentityDbContext(options)
{
    public DbSet<User> DomainUsers { get; } = default!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(InfrastructureAssemblyMarker.Assembly);
    }
}