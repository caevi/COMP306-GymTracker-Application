namespace GymTracker.Core.DTOs.Workouts
{
    public class WorkoutReadDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; } = "";
    }
}
