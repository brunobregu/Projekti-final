using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Academy.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Fusha {0} duhet te plotesohet.")]
        [StringLength(20, ErrorMessage = "Fusha {0} nuk duhet te kete me shume se 20 karaktere.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Fusha {0} duhet te plotesohet.")]
        [StringLength(20, ErrorMessage = "Fusha {0} nuk duhet te kete me shume se 20 karaktere.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Fusha {0} duhet te plotesohet.")]
        [MinLength(8, ErrorMessage = "Fusha {0} duhet te kete me shume se 8 karaktere.")]
        public string Username { get; set; }

        [MinLength(5, ErrorMessage = "Fusha {0} duhet te kete me shume se 5 karaktere.")]
        [Required(ErrorMessage = "Fusha {0} duhet te plotesohet."), DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password-i dhe Konfirmimi i tij duhet te njejte."), DataType(DataType.Password)]
        [Display(Name = "Konfirmo Password")]
        public string ConfirmPassword { get; set; }

        [EmailAddress(ErrorMessage = "Fusha {0} duhet te jete nje adrese e vlefshme emaili.")]
        [Required(ErrorMessage = "Fusha {0} duhet te plotesohet.")]
        public string Email { get; set; }
    }
}