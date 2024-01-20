using System;
using System.Collections.Generic;

namespace FamilyPlanning_API.Models
{
    public partial class WebUser
    {
        public WebUser()
        {
            FamilyMembers = new HashSet<FamilyMember>();
        }

        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; }
        public string Surname { get; set; } = null!;
        public int RoleId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime Birthdate { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<FamilyMember> FamilyMembers { get; set; }
    }
}
