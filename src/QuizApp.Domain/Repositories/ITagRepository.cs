﻿using QuizApp.Domain.Entities;

namespace QuizApp.Domain.Repositories;

public interface ITagRepository : IRepository<Tag>
{
    ValueTask<IList<Tag>> GetAllTagsWithQuestions();
}
