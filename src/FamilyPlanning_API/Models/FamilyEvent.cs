using System;
using System.Collections.Generic;

namespace FamilyPlanning_API.Models
{
    public partial class FamilyEvent
    {
        public int Id { get; set; }
        public int? EventId { get; set; }
        public int? FamilyId { get; set; }

        public virtual BasicEvent? Event { get; set; }
        public virtual Family? Family { get; set; }
    }
}
