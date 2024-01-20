using System;
using System.Collections.Generic;

namespace FamilyPlanning_API.Models
{
    public partial class ListItem
    {
        public int Id { get; set; }
        public int? ListId { get; set; }
        public string? Content { get; set; }
        public bool? Completed { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
