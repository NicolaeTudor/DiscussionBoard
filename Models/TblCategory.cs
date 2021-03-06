﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectDAW.Models
{
    public class TblCategory
    {
        public TblCategory()
        {
            TblPost = new HashSet<TblPost>();
        }

        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<TblPost> TblPost { get; set; }
    }
}