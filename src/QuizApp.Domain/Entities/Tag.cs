namespace QuizApp.Domain.Entities;

public class Tag
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public Guid TesterId { get; set; }

    public User User { get; set; }
    public ICollection<Question> Questions { get; set; }
}