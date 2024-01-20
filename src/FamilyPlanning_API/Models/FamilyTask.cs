using System;
using System.Collections.Generic;

namespace FamilyPlanning_API.Models
{
    public partial class FamilyTask
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public int FamilyId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Family Family { get; set; } = null!;
        public virtual Task Task { get; set; } = null!;
    }
}
