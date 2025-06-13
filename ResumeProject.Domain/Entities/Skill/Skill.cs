using ResumeProject.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ResumeProject.Domain.Entities
{
    public class Skill : IEntityBase
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ResumeId { get; set; } // Foreign key to the Resume entity
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int ProficiencyLevel { get; set; } // 1 to 5 scale, where 1 is beginner and 5 is expert
        public Resume Resume { get; set; } = null!; // Navigation property to the Resume entity
    }
}
