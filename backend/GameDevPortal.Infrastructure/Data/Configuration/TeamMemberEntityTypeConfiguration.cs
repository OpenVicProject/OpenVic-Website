using GameDevPortal.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace GameDevPortal.Infrastructure.Data.Configuration;

public class TeamMemberEntityTypeConfiguration : IEntityTypeConfiguration<TeamMember>
{
    public void Configure(EntityTypeBuilder<TeamMember> builder)
    {
        builder.HasKey(tm => new { tm.UserId, tm.ProjectId });

        builder.HasOne(tm => tm.User).WithMany(u => u.TeamMemberships).HasForeignKey(tm => tm.UserId);
        builder.HasOne(tm => tm.Project).WithMany(p => p.TeamMembers).HasForeignKey(tm => tm.ProjectId);
    }
}
