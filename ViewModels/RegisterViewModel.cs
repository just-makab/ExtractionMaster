using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace EM_WebApp.ViewModels
{
    public class RegisterViewModel : IValidatableObject
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Phone number validation: must include a country code
            if (!Regex.IsMatch(PhoneNumber, @"^\+\d{1,3}\d{6,14}$"))
            {
                yield return new ValidationResult(
                    "Phone number must include a valid country code (e.g., +123456789).",
                    new[] { nameof(PhoneNumber) }
                );
            }

            // Password strength validation
            if (!Regex.IsMatch(Password, @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_]).{8,}$"))
            {
                yield return new ValidationResult(
                    "Password must be at least 8 characters long and include an uppercase letter, a lowercase letter, a number, and a special character.",
                    new[] { nameof(Password) }
                );
            }
        }
    }
}
