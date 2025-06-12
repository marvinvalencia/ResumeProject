namespace ResumeProject.API.Models
{
    public class ExperienceModel
    {
        public Guid Id { get; set; }
        public string Position { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; } // Nullable to allow for ongoing positions
    }
}
