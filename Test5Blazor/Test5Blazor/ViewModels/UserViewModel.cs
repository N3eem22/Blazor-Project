using System.ComponentModel.DataAnnotations;
using Twilio.Types;

namespace Test5Blazor.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        public string FName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        public string LName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid Phone Number.")]
        public string PhoneNumber { get; set; }
    }
}
