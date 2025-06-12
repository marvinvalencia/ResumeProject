namespace ResumeProject.API.Models
{
    public class ResumeModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public List<SkillModel> Skills { get; set; } = new List<SkillModel>();
        public List<ExperienceModel> Experiences { get; set; } = new List<ExperienceModel>();
        public List<EducationModel> Educations { get; set; } = new List<EducationModel>();
        public string Interests { get; set; } = string.Empty;
    }
}
