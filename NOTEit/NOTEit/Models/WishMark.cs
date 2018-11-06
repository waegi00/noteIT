using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NOTEit.Models
{
    public class WishMark
    {
        public int Id { get; set; }

        [Required, Range(1, 6, ErrorMessage = "Die Note muss zwischen 1 und 6 sein."), Display(Name = "Wunschnote")]
        public double WishGrade { get; set; }

        [Required, Range(1, int.MaxValue), Display(Name = "Anzahl Noten")]
        public int Amount { get; set; }

        [Required, Display(Name = "Fach")]
        public virtual Subject Subject { get; set; }

        [Required, Display(Name = "Semester")]
        public virtual Semester Semester { get; set; }
        
        [Display(Name = "Durchschnitt")]
        public virtual ICollection<Mark> Marks { get; set; }
    }
}