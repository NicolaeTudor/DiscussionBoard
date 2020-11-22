using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectDAW.Models
{
    public class TblComment
    {
        public int CommentId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Content { get; set; }
        public int OriginalPostId { get; set; }
        public virtual TblPost OriginalPost { get; set; }
    }
}