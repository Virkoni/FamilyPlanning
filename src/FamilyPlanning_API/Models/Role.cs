using System;
using System.Collections.Generic;

namespace FamilyPlanning_API.Models
{
    public partial class Role
    {
        public Role()
        {
            FamilyMembers = new HashSet<FamilyMember>();
            WebUsers = new HashSet<WebUser>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<FamilyMember> FamilyMembers { get; set; }
        public virtual ICollection<WebUser> WebUsers { get; set; }
    }
}
