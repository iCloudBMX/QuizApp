using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizApp.Domain.Entities;
using QuizApp.Infrastructure.Persistence.Constants;

namespace QuizApp.Infrastructure.Persistence.Configurations;

internal sealed class OtpCodeConfiguration : IEntityTypeConfiguration<OtpCode>
{
    public void Configure(EntityTypeBuilder<OtpCode> builder)
    {
        builder.ToTable(TableNames.OtpCodes);

        builder.HasKey(p => p.Id);

        builder
            .Property(p => p.Code)
            .HasMaxLength(6)
            .IsRequired(true);

        builder
            .Property(p => p.CreatedAt)
            .IsRequired(true);

        builder
            .Property(p => p.Status)
            .IsRequired(true);

        builder
            .HasOne(p => p.User)
            .WithMany(p => p.OtpCodes)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}