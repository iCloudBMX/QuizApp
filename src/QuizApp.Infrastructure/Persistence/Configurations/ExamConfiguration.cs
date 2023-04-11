using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizApp.Domain.Entities;

namespace QuizApp.Infrastructure.Persistence.Configurations
{
    public class ExamConfiguration : IEntityTypeConfiguration<Exam>
    {
        public void Configure(EntityTypeBuilder<Exam> builder)
        {
            builder.ToTable("Exams");

            builder.HasKey(x => x.Id);

            builder.Property(e => e.StartsAt)
                .IsRequired();
                
            builder.Property(e => e.EndsAt)
                .IsRequired();

            builder.Property(e=>e.Duration)
                .IsRequired();

            builder.Property(e => e.Link)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e=>e.QuestionCount) 
                .IsRequired();

            builder.HasOne(e => e.Tester)
                .WithMany(u => u.Exams)
                .HasForeignKey(e => e.TesterId);
        }
    }
}
