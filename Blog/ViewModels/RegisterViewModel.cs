using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
{
    /// <summary>
    /// View model captures on register data from front-end.
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        /// User email.
        /// </summary>
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// User password.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Password confirmation.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
