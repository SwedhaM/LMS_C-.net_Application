using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models
{
    public class Register
    {

        [Required(ErrorMessage = "First Name field is required.")]
        [StringLength(99)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name field is required.")]
        [StringLength(99)]
        public string LastName { get; set; }
        //[RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only Alphabets are Allowed")]
        [Required(ErrorMessage = "Email field is required.")]
        [StringLength(99)]
        //[StringLength(maximumLength: 100, MinimumLength = 2)]
        [EmailAddress]
        public string EmailID { get; set; }

        /*[Required(ErrorMessage = "Phone field is required.")]
        [StringLength(maximumLength: 15, MinimumLength = 10)]
        public string Phone { get; set; }

        [StringLength(maximumLength: 250)]*/
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings =false,ErrorMessage = "Password field is required.")]
        public string Password { get ; set; }
        //[StringLength(maximumLength: 15, MinimumLength = 5)]
        
        
        [DataType(DataType.Password)]
        [Required(ErrorMessage="Please Enter Confirm Password")]
        [Compare("Password",ErrorMessage= "Both Password and Confirm Password Must be Same" )]
        public string ConfirmPassword { get; set; }
        public bool KeepLoggedIn{get;set;}

        
    }
}