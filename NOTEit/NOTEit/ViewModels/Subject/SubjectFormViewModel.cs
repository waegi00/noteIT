using System.ComponentModel.DataAnnotations;

namespace NOTEit.ViewModels.Subject
{
    public class SubjectFormViewModel
    {
        public int Id { get; set; }

        [Required, Display(Name = "Name")]
        public string Name { get; set; }
    }
}