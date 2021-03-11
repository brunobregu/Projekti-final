using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Academy.ViewModels
{
    public class AddNews
    {
        public int Id { get; set; }



        [Required(ErrorMessage = "Fusha {0} eshte e kerkuar")]
        [MaxLength(30, ErrorMessage = "Fusha {0} nuk duhet te jete e gjate me shume se 30 karaktere")]
        public string Title { get; set; }


        [Required(ErrorMessage = "Fusha {0} eshte e kerkuar")]
        [MaxLength(30, ErrorMessage = "Fusha {0} nuk duhet te jete e gjate me shume se 30 karaktere")]
        public string Subtitle { get; set; }


        public string Status { get; set; }


        [Required(ErrorMessage = "Fusha {0} eshte e kerkuar")]
        public string Description { get; set; }


        public string Filename { get; set; }

        public int CategoryId { get; set; }


        public DateTime CreatedOn { get; set; }


        public string UserId { get; set; }
    }
}