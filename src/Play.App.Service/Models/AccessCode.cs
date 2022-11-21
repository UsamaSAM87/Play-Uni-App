using System;
using System.Collections.Generic;

namespace Play.App.Service.Models
{
    public partial class AccessCode
    {
        public int? AccessCode { get; set; }
        public int TeacherId { get; set; }

        public virtual Teacher? TeacherNavigation { get; set; }
    }
}