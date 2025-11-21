namespace GymTracker.Core.Entities
{
    public class User
    {
        public int Id { get; set; }          // PK
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
    }
}
