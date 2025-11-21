using GymTracker.Api.Data;
using GymTracker.Core.Entities;
using GymTracker.Core.Repositories;

namespace GymTracker.Api.Repositories
{
    public class WorkoutRepository : RepositoryBase<Workout>, IWorkoutRepository
    {
        public WorkoutRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
