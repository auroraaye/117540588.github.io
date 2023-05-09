using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace cyber_project.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Required]
        [RegularExpression(".+\\@.+\\..+",ErrorMessage="Please enter valid email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$", ErrorMessage ="Password must contain at least 8 characters and must have 1 alphabet and 1 number")]
        public string Password { get; set; }
    }
}