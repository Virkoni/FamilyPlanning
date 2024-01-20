using System;
using System.Collections.Generic;

namespace FamilyPlanning_API.Models
{
    public partial class FamilyMember
    {
        public int Id { get; set; }
        public int FamilyId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Family Family { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
        public virtual WebUser User { get; set; } = null!;
    }
}
