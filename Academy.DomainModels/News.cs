using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.DomainModels
{
    public class News
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Title { get; set; }


        [Required]
        [MaxLength(30)]
        public string Subtitle { get; set; }


        [Required]
        public string Status { get; set; }

        [Required]
        public string Description { get; set; }


        [Required]
        public string Filename { get; set; }

        public string Tag { get; set; }


        public DateTime CreatedOn { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }


        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
