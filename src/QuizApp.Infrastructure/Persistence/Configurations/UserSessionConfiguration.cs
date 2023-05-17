using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizApp.Domain.Entities;
using QuizApp.Infrastructure.Persistence.Constants;

namespace QuizApp.Infrastructure.Persistence.Configurations;

public class UserSessionConfiguration : IEntityTypeConfiguration<UserSession>
{
    public void Configure(EntityTypeBuilder<UserSession> builder)
    {
        builder
            .ToTable(TableNames.UserSessions);

        builder
            .HasKey(t => t.Id);
        builder
            .HasOne(t => t.User)
            .WithMany(u => u.UserSessions)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .Property(p => p.Token)
            .IsRequired(true);

        builder
            .Property(p => p.Status)
            .IsRequired(true);

        builder
            .Property(p => p.CreatedAt)
            .IsRequired(true);

    }
}
