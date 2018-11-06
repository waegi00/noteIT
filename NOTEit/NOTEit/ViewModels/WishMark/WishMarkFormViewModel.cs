using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NOTEit.ViewModels.WishMark
{
    public class WishMarkFormViewModel
    {
        public int Id { get; set; }

        [Required, Range(1, 6, ErrorMessage = "Die Note muss zwischen 1 und 6 sein."), Display(Name = "Wunschnote")]
        public double WishGrade { get; set; }

        [Required, Range(1, int.MaxValue), Display(Name = "Anzahl Noten")]
        public int Amount { get; set; }

        [Required, Display(Name = "Fach")]
        public int Subject { get; set; }

        public virtual ICollection<Models.Subject> AllSubjects { get; set; }

        [Required, Display(Name = "Semester")]
        public int Semester { get; set; }

        public virtual ICollection<Models.Semester> AllSemesters { get; set; }
    }
}