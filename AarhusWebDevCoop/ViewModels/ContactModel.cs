using System.ComponentModel.DataAnnotations;

namespace AarhusWebDevCoop.ViewModels
{
    public class ContactModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "This is not a valid email address")]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }
        
    }
}