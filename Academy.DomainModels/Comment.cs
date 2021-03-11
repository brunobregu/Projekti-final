using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.DomainModels
{
    public class Comment
    {
        public int Id { get; set; }


        [Required]
        public string FullName { get; set; }



        [EmailAddress]
        [Required]
        public string Email { get; set; }


        [Required]
        public string Description { get; set; }



        public DateTime CreatedOn { get; set; }


        [ForeignKey("News")]
        public int NewsId { get; set; }
        public virtual News News { get; set; }
    }
}
