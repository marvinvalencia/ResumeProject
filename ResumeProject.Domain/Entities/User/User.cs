using Microsoft.AspNetCore.Identity;
using ResumeProject.Domain.Enum;

namespace ResumeProject.Domain.Entities
{
    public class User : IdentityUser
    {
        public Role Role { get; set; }
    }
}
