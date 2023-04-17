using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizApp.Domain.Entities;

namespace QuizApp.Infrastructure.Persistence.Configurations;

public class ExamAttendantConfiguration : IEntityTypeConfiguration<ExamAttendant>
{
    public void Configure(EntityTypeBuilder<ExamAttendant> builder)
    {
        builder
            .ToTable("ExamAttendants");

        builder
            .HasKey(e => e.Id);

        builder
            .Property(examAttendant => examAttendant.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(exaAttendant => exaAttendant.Token)
            .IsRequired()
            .HasMaxLength(150);

        builder
            .Property(examAttendant => examAttendant.Score)
            .IsRequired();

        builder
            .HasOne(x => x.Exam)
            .WithMany(y => y.Attendants)
            .HasForeignKey(x => x.ExamId);

    }
}
