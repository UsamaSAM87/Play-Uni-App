using System;
using System.Collections.Generic;

namespace Play.App.Service.Models
{
    public partial class Exam
    {
        public int? StudentId { get; set; }
        public int? CourseId { get; set; }
        public int? Grade { get; set; }
        public DateTime? Date { get; set; }
        public string? Honors { get; set; }

        public virtual Course? Course { get; set; }
        public virtual Student? Student { get; set; }
    }
}
