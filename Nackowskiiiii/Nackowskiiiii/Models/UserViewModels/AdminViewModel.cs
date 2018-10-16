using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowskiiiii.Models.UserViewModels
{
    public class AdminViewModel
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "Email is mandatory")]
        public string Email { get; set; }

        [Required (ErrorMessage = "Password is mandatory")]
        public string Password { get; set; }

        [Display (Name = "Confirmation password")]
        [Compare ("Password", ErrorMessage = "Password and ConfirmationPassword must match")]
        public string ConfirmationPassword { get; set; }
    }
}
