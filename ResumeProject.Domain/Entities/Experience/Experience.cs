using ResumeProject.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ResumeProject.Domain.Entities
{
    public class Experience : IEntityBase
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ResumeId { get; set; } // Foreign key to the Resume entity
        public string Position { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; } // Nullable to allow for ongoing positions
        public Resume? Resume { get; set; } = null; // Navigation property to the Resume entity
    }
}
