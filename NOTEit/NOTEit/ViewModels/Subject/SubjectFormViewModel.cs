using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NOTEit.ViewModels.Subject
{
    public class SubjectFormViewModel
    {
        public int Id { get; set; }

        [Required, Display(Name = "Name")]
        public string Name { get; set; }

        [Required, Display(Name = "Semester")]
        public ICollection<int> Semesters { get; set; } = new List<int>();

        public virtual ICollection<Models.Semester> AllSemesters { get; set; }
    }
}