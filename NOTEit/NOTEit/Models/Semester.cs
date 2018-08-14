using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NOTEit.Models
{
    public class Semester
    {
        public int Id { get; set; }

        [Required, Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Fächer")]
        public virtual ICollection<Subject> Subjects { get; set; }

        public virtual ApplicationUser Owner { get; set; }
    }
}