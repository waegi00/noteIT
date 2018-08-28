using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NOTEit.ViewModels.Mark
{
    public class MarkFormViewModel
    {
        public int Id { get; set; }

        [Required, Range(1, 6, ErrorMessage = "Die Note muss zwischen 1 und 6 sein."), Display(Name = "Note")]
        public double Grade { get; set; }

        [Required, Display(Name = "Fach")]
        public int Subject { get; set; }

        public virtual ICollection<Models.Subject> AllSubjects { get; set; }

        [Required, Display(Name = "Semester")]
        public int Semester { get; set; }

        public virtual ICollection<Models.Semester> AllSemesters { get; set; }
    }
}