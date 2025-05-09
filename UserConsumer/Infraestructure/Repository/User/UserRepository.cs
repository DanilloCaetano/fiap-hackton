using Domain.User.Model;
using Domain.User.Repository;
using Infraestructure.Repository.Base;

namespace Infraestructure.Repository.User
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        private readonly Context.TechChallengeContext _context;

        public UserRepository(Context.TechChallengeContext context) : base(context)
        {
            _context = context;
        }
    }
}
