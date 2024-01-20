using System;
using System.Collections.Generic;

namespace FamilyPlanning_API.Models
{
    public partial class CalendarEvent
    {
        public int Id { get; set; }
        public int? CalendarId { get; set; }
        public int? EventId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Calendar? Calendar { get; set; }
        public virtual BasicEvent? Event { get; set; }
    }
}
