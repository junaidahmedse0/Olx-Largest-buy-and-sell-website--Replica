using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OlxProject.Models
{
    public class UserViewModel
    {



        public int Id { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        [Display(Name = "Confirm")]
        [Compare("Password")]
        public string  ConfirmPassword{ get; set; }
        [Required(ErrorMessage = "Mobile no. is required")]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{11,11}$", ErrorMessage = "Please enter valid phone no.")]
        public string Contact { get; set; }
       
        //public  HttpPostedFileBase File { get; set; }

        [Required]
        public string Info { get; set; }

        public UserRole UserRole { get; set; }

       
    }
}