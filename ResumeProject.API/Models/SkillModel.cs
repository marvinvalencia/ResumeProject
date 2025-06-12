namespace ResumeProject.API.Models
{
    public class SkillModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int ProficiencyLevel { get; set; } // 1 to 5 scale, where 1 is beginner and 5 is expert
    }
}
