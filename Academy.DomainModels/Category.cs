using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.DomainModels
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }


        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }



        public virtual ICollection<News> News { get; set; }
    }
}
