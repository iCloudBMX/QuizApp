using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizApp.Domain.Entities;

namespace QuizApp.Infrastructure.Persistence.Configurations;
public class QuestionOptionsConfiguration : IEntityTypeConfiguration<QuestionOption>
{
    public void Configure(EntityTypeBuilder<QuestionOption> builder)
    {
        builder
            .ToTable(nameof(QuestionOption));

        builder
            .HasKey(qo => qo.Id);

        builder
            .Property(qo => qo.IsCorrect)
            .IsRequired(true);

        builder
            .Property(qo => qo.Content)
            .IsRequired(true);

        builder
            .HasOne(qo => qo.Question)
            .WithMany(question => question.QuestionOptions)
            .HasForeignKey(qo => qo.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
