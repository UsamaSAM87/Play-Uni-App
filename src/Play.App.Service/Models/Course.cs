using System;
using System.Collections.Generic;

namespace Play.App.Service.Models
{
    public partial class Course
    {
        public int CourseId { get; set; }
        public string? Name { get; set; }
        public int? Cfu { get; set; }
        public int? Teacher { get; set; }

        public virtual Teacher? TeacherNavigation { get; set; }
    }
}
