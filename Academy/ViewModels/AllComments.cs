using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Academy.ViewModels
{
    public class AllComments
    {
        public int Id { get; set; }


        public string FullName { get; set; }



        public string Email { get; set; }


        public string Description { get; set; }



        public DateTime CreatedOn { get; set; }


        public int NewsId { get; set; }
    }
}