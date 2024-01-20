using System;
using System.Collections.Generic;

namespace FamilyPlanning_API.Models
{
    public partial class Task
    {
        public Task()
        {
            FamilyTasks = new HashSet<FamilyTask>();
        }

        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public int AssignedTo { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<FamilyTask> FamilyTasks { get; set; }
    }
}
