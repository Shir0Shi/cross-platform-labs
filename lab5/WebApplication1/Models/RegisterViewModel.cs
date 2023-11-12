using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "Імя користувача має бути не більше 50 символів.")]
        [Remote(action: "IsUserNameInUse", controller: "Account")]
        public string Username { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "ФІО має бути не більше 500 символів.")]
        public string FullName { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "Пароль має бути від 8 до 16 символів.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).+$", ErrorMessage = "Пароль має містити щонайменше 1 цифру, 1 знак, 1 велику літеру.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Паролі не співпадають.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Phone(ErrorMessage = "Неправильний формат телефонного номеру.")]
        [RegularExpression(@"^\+380\d{9}$", ErrorMessage = "Телефон має відповідати формату України.")]
        public string Phone { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Неправильний формат електронної адреси.")]
        public string Email { get; set; }
    }
}
