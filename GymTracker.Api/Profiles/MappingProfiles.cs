using AutoMapper;
using GymTracker.Core.DTOs.Users;
using GymTracker.Core.DTOs.Exercises;
using GymTracker.Core.DTOs.Workouts;
using GymTracker.Core.Entities;

namespace GymTracker.Api.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User
            CreateMap<User, UserReadDto>();
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();

            // Exercise
            CreateMap<Exercise, ExerciseReadDto>();
            CreateMap<ExerciseCreateDto, Exercise>();
            CreateMap<ExerciseUpdateDto, Exercise>();

            // Workout
            CreateMap<Workout, WorkoutReadDto>();
            CreateMap<WorkoutCreateDto, Workout>();
            CreateMap<WorkoutUpdateDto, Workout>();
        }
    }
}
