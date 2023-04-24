using QuizApp.Domain.Entities;
using QuizApp.Domain.Repositories;

namespace QuizApp.Infrastructure.Persistence.Repositories;

internal sealed class OtpCodeRepository : Repository<OtpCode>, IOtpCodeRepository
{
	public OtpCodeRepository(ApplicationDbContext applicationDbContext)
		: base(applicationDbContext)
	{
	}
}
