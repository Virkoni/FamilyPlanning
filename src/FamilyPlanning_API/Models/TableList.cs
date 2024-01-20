using System;
using System.Collections.Generic;

namespace FamilyPlanning_API.Models
{
    public partial class TableList
    {
        public TableList()
        {
            FamilyLists = new HashSet<FamilyList>();
        }

        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<FamilyList> FamilyLists { get; set; }
    }
}
