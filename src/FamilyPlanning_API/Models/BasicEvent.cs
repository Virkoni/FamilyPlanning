using System;
using System.Collections.Generic;

namespace FamilyPlanning_API.Models
{
    public partial class BasicEvent
    {
        public BasicEvent()
        {
            CalendarEvents = new HashSet<CalendarEvent>();
            FamilyEvents = new HashSet<FamilyEvent>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Location { get; set; } = null!;
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<CalendarEvent> CalendarEvents { get; set; }
        public virtual ICollection<FamilyEvent> FamilyEvents { get; set; }
    }
}
