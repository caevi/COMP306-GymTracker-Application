namespace GymTracker.Core.DTOs.Workouts
{
    public class WorkoutCreateDto
    {
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; } = "";
    }
}
