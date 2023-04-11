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
            .WithMany(ex => ex.Attendants)
            .HasForeignKey(attendant => attendant.ExamId);
        
        builder
            .HasOne(attendant => attendant.Question)
            .WithMany(question => question.AttendantAnswers)
            .HasForeignKey(answer => answer.QuestionId);

        builder
            .HasOne(attendant => attendant.Option)
            .WithMany(option => option.AttendantsAnswers)
            .HasForeignKey(attendant => attendant.OptionId);

        builder
            .HasOne(attendant => attendant.ExamAttendant)
            .WithMany(option => option.AttendantAnswers)
            .HasForeignKey(attendant => attendant.ExamAttendantsId);

    }
}
