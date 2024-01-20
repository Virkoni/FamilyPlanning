using System;
using System.Collections.Generic;

namespace FamilyPlanning_API.Models
{
    public partial class Family
    {
        public Family()
        {
            FamilyEvents = new HashSet<FamilyEvent>();
            FamilyLists = new HashSet<FamilyList>();
            FamilyMembers = new HashSet<FamilyMember>();
            FamilyTasks = new HashSet<FamilyTask>();
        }

        public int Id { get; set; }
        public string FamilyName { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<FamilyEvent> FamilyEvents { get; set; }
        public virtual ICollection<FamilyList> FamilyLists { get; set; }
        public virtual ICollection<FamilyMember> FamilyMembers { get; set; }
        public virtual ICollection<FamilyTask> FamilyTasks { get; set; }
    }
}
