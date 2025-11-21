using GymTracker.Api.Data;
using GymTracker.Core.Entities;
using GymTracker.Core.Repositories;

namespace GymTracker.Api.Repositories
{
    public class ExerciseRepository : RepositoryBase<Exercise>, IExerciseRepository
    {
        public ExerciseRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
