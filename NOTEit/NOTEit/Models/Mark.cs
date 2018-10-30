using System.ComponentModel.DataAnnotations;

namespace NOTEit.Models
{
    public class Mark
    {
        public int Id { get; set; }

        [Required, Range(1, 6, ErrorMessage = "Die Note muss zwischen 1 und 6 sein."), Display(Name = "Note")]
        public double Grade { get; set; }

        [Required, Display(Name = "Fach")]
        public virtual Subject Subject { get; set; }

        [Required, Display(Name = "Semester")]
        public virtual Semester Semester { get; set; }
    }
}