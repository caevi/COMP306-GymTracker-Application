using GymTracker.Api.Data;
using GymTracker.Core.Entities;
using GymTracker.Core.Repositories;

namespace GymTracker.Api.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
