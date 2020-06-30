using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertApi.DTOs
{
    public class CreateClientRequest
    {
        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Login is required")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
