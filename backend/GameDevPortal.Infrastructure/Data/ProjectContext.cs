using GameDevPortal.Core.Entities;
using GameDevPortal.Core.Interfaces;
using GameDevPortal.Core.Models;
using GameDevPortal.Infrastructure.Data.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace GameDevPortal.Infrastructure.Data;

public class ProjectContext : IdentityDbContext<User, UserRole, Guid>
{
    public DbSet<TeamMember> TeamMembers => Set<TeamMember>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<ProgressReport> ProgressReports => Set<ProgressReport>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Faq> Faqs => Set<Faq>();

    public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        new UserEntityTypeConfiguration().Configure(builder.Entity<User>());
        new ProjectEntityTypeConfiguration().Configure(builder.Entity<Project>());
        new TeamMemberEntityTypeConfiguration().Configure(builder.Entity<TeamMember>());
        new UserRoleEntityTypeConfiguration().Configure(builder.Entity<UserRole>());
        base.OnModelCreating(builder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries().Where(e => e.Entity is IEntity).ToList())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    var timeStamp = DateTime.UtcNow;
                    entry.Property(nameof(IEntity.CreatedAt)).CurrentValue = timeStamp;
                    entry.Property(nameof(IEntity.UpdatedAt)).CurrentValue = timeStamp;
                    break;
                case EntityState.Modified:
                    entry.Property(nameof(IEntity.UpdatedAt)).CurrentValue = DateTime.UtcNow;
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}

public class ProjectContextFactory : IDesignTimeDbContextFactory<ProjectContext>
{
    public ProjectContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ProjectContext>();

        IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build();

        optionsBuilder.UseSqlServer(config.GetConnectionString("ProjectDBConnectionString"));

        return new ProjectContext(optionsBuilder.Options);
    }
}