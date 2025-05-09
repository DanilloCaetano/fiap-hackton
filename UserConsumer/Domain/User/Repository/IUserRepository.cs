using Domain.Base.Repository;
using Domain.User.Model;

namespace Domain.User.Repository
{
    public interface IUserRepository : IBaseRepository<UserEntity>
    {
    }
}
