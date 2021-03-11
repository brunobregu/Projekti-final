using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Academy.ViewModels
{
    public class EditUserVM
    {
        public string Id { get; set; }


        [Required(ErrorMessage ="Fusha {0} eshte e kerkuar")]
        [StringLength(20, ErrorMessage ="Fusha {0} nuk duhet te permbaje me shume se 20 karaktere")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Fusha {0} eshte e kerkuar")]
        [StringLength(20, ErrorMessage = "Fusha {0} nuk duhet te permbaje me shume se 20 karaktere")]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage ="Fusha {0} duhet te jete nje adrese e vlefshme email")]
        [Required(ErrorMessage = "Fusha {0} eshte e kerkuar")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Fusha {0} eshte e kerkuar")]
        [MinLength(8, ErrorMessage = "Fusha {0} duhet te kete me shume se 8 karaktere.")]
        public string Username { get; set; }
    }
}