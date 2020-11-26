using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectDAW.Models
{
    public class TblPost
    {
        public TblPost()
        {
            TblComments = new HashSet<TblComment>();
        }

        [Key]
        public int PostId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [ForeignKey(nameof(Category))]
        [Required]
        public int CategoryId { get; set; }

        public virtual TblCategory Category { get; set; }
        
        public virtual ICollection<TblComment> TblComments { get; set; }
    }
}