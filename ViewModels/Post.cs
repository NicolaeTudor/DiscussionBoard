using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectDAW.ViewModels
{
    public class Post
    {
        public int PostId { get; set; }
        public int CategoryId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
    }
}