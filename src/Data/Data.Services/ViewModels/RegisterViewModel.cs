namespace Data.Services.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterViewModel
    {
        public string Email { get; set; }

        [Compare("ConfirmPassword")]
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
