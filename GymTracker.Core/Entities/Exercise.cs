namespace GymTracker.Core.Entities
{
    public class Exercise
    {
        public int Id { get; set; }          // PK
        public string Name { get; set; } = "";
        public string MuscleGroup { get; set; } = "";  // e.g. Chest, Back, Legs
    }
}
