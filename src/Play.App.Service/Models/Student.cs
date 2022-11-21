using System;
using System.Collections.Generic;

namespace Play.App.Service.Models
{
    public partial class Student
    {
        public int MatriculationId { get; set; }
        public string? Surname { get; set; }
        public string? Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? RegistrationDate { get; set; }
    }
}
