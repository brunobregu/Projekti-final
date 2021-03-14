using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Academy.ViewModels
{
    public class ChangePasswordVM
    {
        [Display(Name = "Passwordi i vjeter")]
        [Required(ErrorMessage = "Fusha {0} eshte e kerkuar")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }



        [Display(Name = "Passwordi i ri")]
        [Required(ErrorMessage = "Fusha {0} eshte e kerkuar")]
        [MinLength(5, ErrorMessage = "Fusha {0} duhet te kete me shume se 5 karaktere")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }



        [Display(Name = "Konfirmo Password-in")]
        [Required(ErrorMessage = "Fusha {0} eshte e kerkuar")]
        [DataType(DataType.Password)]
        [Compare(otherProperty: "NewPassword", ErrorMessage = "Passwordi i ri dhe Konfirmimi i tij nuk jane te njejta")]
        public string ConfirmPassword { get; set; }
    }
}