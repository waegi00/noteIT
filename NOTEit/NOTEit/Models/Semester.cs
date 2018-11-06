using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NOTEit.Models
{
    public class Semester
    {
        public int Id { get; set; }

        [Required, Display(Name = "Name")]
        public string Name { get; set; }

        [Required, Display(Name = "Fächer")]
        public virtual ICollection<Subject> Subjects { get; set; }

        public virtual ICollection<WishMark> WishMarks { get; set; }
    }
}