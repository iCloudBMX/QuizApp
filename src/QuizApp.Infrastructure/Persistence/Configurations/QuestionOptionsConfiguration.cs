using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizApp.Domain.Entities;
using QuizApp.Infrastructure.Persistence.Constants;

namespace QuizApp.Infrastructure.Persistence.Configurations;
public class QuestionOptionsConfiguration : IEntityTypeConfiguration<QuestionOption>
{
    public void Configure(EntityTypeBuilder<QuestionOption> builder)
    {
        builder
            .ToTable(TableNames.QuestionOptions);

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
