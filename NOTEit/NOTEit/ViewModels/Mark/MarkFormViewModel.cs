using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NOTEit.ViewModels.Mark
{
    public class MarkFormViewModel
    {
        public int Id { get; set; }

        [Required, Range(1, 6, ErrorMessage = "Die Note muss zwischen 1 und 6 sein."), Display(Name = "Note")]
        public double Grade { get; set; }

        [Required, Display(Name = "Fach")] public virtual Models.Subject Subject { get; set; }
        public virtual ICollection<Models.Subject> AllSubjects { get; set; }

        [Required, Display(Name = "Semester")] public virtual Models.Semester Semester { get; set; }

        public virtual ICollection<Models.Semester> AllSemesters { get; set; }
    }
}