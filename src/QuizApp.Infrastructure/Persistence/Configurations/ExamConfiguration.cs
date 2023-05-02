using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizApp.Domain.Entities;
using QuizApp.Infrastructure.Persistence.Constants;

namespace QuizApp.Infrastructure.Persistence.Configurations
{
    public class ExamConfiguration : IEntityTypeConfiguration<Exam>
    {
        public void Configure(EntityTypeBuilder<Exam> builder)
        {
            builder.ToTable(TableNames.Exams);

            builder.HasKey(x => x.Id);

            builder.Property(e => e.StartsAt)
                .IsRequired();

            builder.Property(e => e.EndsAt)
                .IsRequired();

            builder.Property(e => e.Duration)
                .IsRequired();

            builder.Property(e => e.Link)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.QuestionCount)
                .IsRequired();

            builder.HasOne(e => e.Tester)
                .WithMany(u => u.Exams)
                .HasForeignKey(e => e.TesterId);

            builder.HasMany(e => e.Questions)
                .WithMany(q => q.Exams)
                .UsingEntity(
                    l => l.HasOne(typeof(Exam))
                    .WithMany()
                    .HasForeignKey("ExamId")
                    .OnDelete(DeleteBehavior.Cascade),
                    r => r.HasOne(typeof(Question))
                    .WithMany()
                    .HasForeignKey("QuestionId")
                    .OnDelete(DeleteBehavior.NoAction));
        }
    }
}
