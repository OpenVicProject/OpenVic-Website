using GameDevPortal.Core.Entities;
using GameDevPortal.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace GameDevPortal.Infrastructure.Data.Configuration;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Id).HasColumnType("uniqueidentifier");
        builder.HasKey(u => u.Id); 
    }
}
