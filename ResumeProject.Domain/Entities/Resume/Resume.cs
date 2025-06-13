using ResumeProject.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ResumeProject.Domain.Entities
{
    public class Resume : IEntityBase
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public byte[]? Picture { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public ICollection<Skill> Skills { get; set; } = new List<Skill>();
        public ICollection<Experience> Experiences { get; set; } = new List<Experience>();
        public ICollection<Education> Educations { get; set; } = new List<Education>();
        public string Interests { get; set; } = string.Empty;
    }
}
