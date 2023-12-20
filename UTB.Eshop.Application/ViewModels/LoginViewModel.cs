using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BistroWeb.Application.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
        public bool LoginFailed { get; set; }
    }
}
