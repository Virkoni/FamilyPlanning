using System;
using System.Collections.Generic;

namespace FamilyPlanning_API.Models
{
    public partial class FamilyList
    {
        public int Id { get; set; }
        public int ListId { get; set; }
        public int FamilyId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Family Family { get; set; } = null!;
        public virtual TableList List { get; set; } = null!;
    }
}
