using GameDevPortal.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameDevPortal.Infrastructure.Data.Configuration;

public class UserRoleEntityTypeConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasData(new List<UserRole>
        {
            new UserRole()
            {
                Id = new Guid("662ce9ac-052d-4211-ae0d-50883ccdf872"),
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            },
            new UserRole()
            {
                Id = new Guid("094bf682-2c34-4d18-a24f-a20ffbd232be"),
                Name = "Moderator",
                NormalizedName = "MODERATOR"
            },
            new UserRole()
            {
                Id = new Guid("27a71320-dfd4-4833-bbad-5f7fc5670532"),
                Name = "User",
                NormalizedName = "USER"
            }
        });
    }
}