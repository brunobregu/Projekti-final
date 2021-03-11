using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Academy.ViewModels
{
    public class AddCategory
    {
        [Required(ErrorMessage = "Fusha {0} eshte e kerkuar")]
        [StringLength(20, ErrorMessage = "Fusha {0} nuk duhet te kete me shume se 20 karaktere")]
        public string Name { get; set; }

        public int Id { get; set; }
    }
}