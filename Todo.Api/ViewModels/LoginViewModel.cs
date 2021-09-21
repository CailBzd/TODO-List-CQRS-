using System;
using System.ComponentModel.DataAnnotations;

namespace Todo.Api.Models
{
    public class LoginViewModel
    {

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }


    }
}
