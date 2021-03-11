using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Academy.ViewModels
{
    public class AddComment
    {
        public int Id { get; set; }


        [Required(ErrorMessage ="Fusha {0} eshte e kerkuar")]
        public string FullName { get; set; }



        [EmailAddress(ErrorMessage = "Fusha {0} nuk eshte nje adrese e vlefshme email")]
        [Required(ErrorMessage = "Fusha {0} eshte e kerkuar")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Fusha {0} eshte e kerkuar")]
        public string Description { get; set; }



        public DateTime CreatedOn { get; set; }


        public int NewsId { get; set; }
    }
}