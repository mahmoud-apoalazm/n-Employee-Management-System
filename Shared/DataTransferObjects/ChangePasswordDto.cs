using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record ChangePasswordDto
    {
        [Required(ErrorMessage = "User name is required")]
        public string? UserName { get; init; }
        [Required(ErrorMessage = "Password  is required")]
        public string? CurrentPassword { get; init; }
        [Required(ErrorMessage = "New Password is required")]
        public string? NewPassword { get; init; }
        [Required(ErrorMessage = "Confirm New Password is required"),
         Compare("NewPassword",ErrorMessage ="Password not match")]
        public string? ConfirmNewPassword { get; init; }

    }
}
