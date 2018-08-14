using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NOTEit.Models
{
    public class Subject
    {
        public int Id { get; set; }

        [Required, Display(Name = "Name")]
        public string Name { get; set; }
        
        [Display(Name = "Semester")]
        public virtual ICollection<Semester> Semesters { get; set; }
        
        [Display(Name = "Noten")]
        public virtual ICollection<Mark> Marks { get; set; }
    }
}