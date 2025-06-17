using ResumeProject.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ResumeProject.Domain.Entities
{
    public class Education : IEntityBase
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid(); // Assuming you want to implement IEntityBase
        public Guid ResumeId { get; set; } // Foreign key to the Resume entity
        public string Degree { get; set; } = string.Empty;
        public string Institution { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; } // Nullable to allow for ongoing education
        public string Major { get; set; } = string.Empty;
        public double GPA { get; set; } // Optional, can be nullable if not applicable
        public Resume? Resume { get; set; } = null; // Navigation property to the Resume entity
    }
}
