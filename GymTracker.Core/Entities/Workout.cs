namespace GymTracker.Core.Entities
{
    public class Workout
    {
        public int Id { get; set; }              // PK
        public int UserId { get; set; }          // FK → User
        public DateTime Date { get; set; }       // Workout date
        public string Notes { get; set; } = "";  // Optional notes

        // Navigation (optional, useful later)
        public User? User { get; set; }
    }
}
