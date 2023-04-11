using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizApp.Domain.Entities;

namespace QuizApp.Infrastructure.Persistence.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasIndex(u => u.Phone)
            .IsUnique();

        builder.ToTable("Users");

        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(u => u.LastName)
            .HasMaxLength(30);

        builder.Property(u => u.PasswordHash)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.RegisteredAt)
            .IsRequired();

        builder.Property(u => u.LastLogin)
            .IsRequired();
    }
}
