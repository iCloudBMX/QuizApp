using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizApp.Domain.Entities;
using QuizApp.Infrastructure.Persistence.Constants;

namespace QuizApp.Infrastructure.Persistence.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(TableNames.Users);

        builder.HasKey(p => p.Id);

        builder.HasIndex(u => u.Phone)
            .IsUnique(true);

        builder.Property(u => u.FirstName)
            .IsRequired(true)
            .HasMaxLength(50);

        builder.Property(u => u.LastName)
            .IsRequired(true)
            .HasMaxLength(50);

        builder.Property(u => u.PasswordHash)
            .IsRequired(true)
            .HasMaxLength(50);

        builder.Property(u => u.RegisteredAt)
            .IsRequired(true);

        builder.Property(u => u.LastLogin)
            .IsRequired(false);

        builder
            .Property(u => u.Email)
            .IsRequired(true)
            .HasMaxLength(100);

        builder
            .HasIndex(u => u.Email)
            .IsUnique(true);
    }
}
