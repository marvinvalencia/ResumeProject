namespace ResumeProject.Domain.Entities
{
    public class Resume
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public List<Skill> Skills { get; set; } = new List<Skill>();
        public List<Experience> Experiences { get; set; } = new List<Experience>();
        public List<Education> Educations { get; set; } = new List<Education>();
        public string Interests { get; set; } = string.Empty;
    }
}
