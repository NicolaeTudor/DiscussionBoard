using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ProjectDAW.ViewModels;

namespace ProjectDAW.Models
{
    public class TblComment
    {
        [Key]
        public int CommentId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Content { get; set; }
        
        [ForeignKey(nameof(Post))]
        public int PostId { get; set; }
        public virtual TblPost Post { get; set; }
    }
}