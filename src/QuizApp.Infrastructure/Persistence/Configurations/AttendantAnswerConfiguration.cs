using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizApp.Domain.Entities;

namespace QuizApp.Infrastructure.Persistence.Configurations;

public class AttendantAnswerConfiguration : IEntityTypeConfiguration<AttendantAnswer>
{
    public void Configure(EntityTypeBuilder<AttendantAnswer> builder)
    {
        builder
            .ToTable("AttendantAnswers");

        builder
            .HasKey(answer => answer.Id);

        builder
            .HasOne(atans => atans.Exam)
            .WithMany()
            .HasForeignKey(attendant => attendant.ExamId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasOne(attendant => attendant.Question)
            .WithMany(question => question.AttendantAnswers)
            .HasForeignKey(answer => answer.QuestionId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(attendant => attendant.Option)
            .WithMany(option => option.AttendantAnswers)
            .HasForeignKey(attendant => attendant.OptionId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(attendant => attendant.ExamAttendant)
            .WithMany(option => option.AttendantAnswers)
            .HasForeignKey(attendant => attendant.ExamAttendantId)
            .OnDelete(DeleteBehavior.NoAction);

    }
}
