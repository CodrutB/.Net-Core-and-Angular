using System.ComponentModel.DataAnnotations;

namespace CoreAngularPoC.ViewModels
{
    public class ContactViewModel
    {
        [Required]
        [ MinLength(5)]
        public string Name { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        [MaxLength(250, ErrorMessage ="Too Long")]
        public string Message { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
