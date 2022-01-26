namespace Data.Services.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterViewModel
    {
        private const string CONFIRM_PASSWORD_STRING = "ConfirmPassword";

        public string Email { get; set; }

        [Compare(CONFIRM_PASSWORD_STRING)]
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
