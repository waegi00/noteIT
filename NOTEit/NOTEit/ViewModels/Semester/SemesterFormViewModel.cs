using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NOTEit.ViewModels.Semester
{
    public class SemesterFormViewModel
    {
        public int Id { get; set; }

        [Required, Display(Name = "Name")]
        public string Name { get; set; }
        
        [Display(Name = "Fächer")]
        public virtual ICollection<int> Subjects { get; set; } = new List<int>();
        
        public virtual ICollection<Models.Subject> AllSubjects { get; set; }
    }
}