using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models
{
    public class Login
    {
        public bool RememberMe { get; set; }

        [Required(ErrorMessage = "Email field is required.")]
        [EmailAddress]
        public string? EmailID { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password field is required.")]
        public string? Password { get ; set; }
        
        public bool KeepLoggedIn{get;set;}

        
    }
}