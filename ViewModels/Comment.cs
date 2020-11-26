using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectDAW.ViewModels
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
        [Required]
        public string Content { get; set; }
    }
}