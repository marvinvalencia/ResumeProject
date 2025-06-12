namespace ResumeProject.Domain.Entities
{
    public class Education
    {
        public Guid Id { get; set; }
        public string Degree { get; set; } = string.Empty;
        public string Institution { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; } // Nullable to allow for ongoing education
        public string Major { get; set; } = string.Empty;
        public double GPA { get; set; } // Optional, can be nullable if not applicable
    }
}
