using System;
using System.Collections.Generic;

namespace FamilyPlanning_API.Models
{
    public partial class Calendar
    {
        public Calendar()
        {
            CalendarEvents = new HashSet<CalendarEvent>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<CalendarEvent> CalendarEvents { get; set; }
    }
}
