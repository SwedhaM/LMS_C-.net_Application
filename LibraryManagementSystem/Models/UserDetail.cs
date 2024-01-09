 using System;
 using System.Collections.Generic;
 using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace LibraryManagementSystem.Models
{

public class UserDetail
{
    public int ID { get; set; }
    public string? ProfilePicture { get; set; }

    [Required(ErrorMessage = "Register number is required")]
    [RegularExpression(@"^(?!0{1,3})\d{5,9}$", ErrorMessage = "Register Number must be between 5 and 9 digits and it should not contain one or more zeros at First.")]
    public string RegisterNo { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [RegularExpression(@"^(?:[A-Z][a-z]+(?:\s|$))+[A-Z](?:\s[A-Z])*$", ErrorMessage = "Name must be in this format 'Name Initial'")]
    public string Name { get; set; }



    [Required(ErrorMessage = "Email ID is required")]
    [RegularExpression(@"^[a-z][a-z0-9._%+-]+@gmail\.com$", ErrorMessage = "Invalid Email Address")]
    public string EmailID { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W)(?!.*\s).{8,15}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, one symbol, and be 8-15 characters long.")]
    public string Password { get; set; }


    [Required(ErrorMessage = "Gender is required")]
    public string Gender { get; set; }

    [Required(ErrorMessage = "Age is required")]
    [Range(18, int.MaxValue, ErrorMessage = "Age must be above 18.")]
    public int Age { get; set; }

    [Required(ErrorMessage = "Address is required")]
    public string Address { get; set; }

    [Required(ErrorMessage = "Phone number is required")]
    [RegularExpression(@"^[7-9][0-9]{9}$", ErrorMessage = "Please enter a valid mobile number.")]

    public string PhoneNumber { get; set; }

    
}
}