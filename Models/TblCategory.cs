using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectDAW.Models
{
    public class TblCategory
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey(nameof(UserCreated))]
        public int UserCreatedId { get; set; }

        public virtual TblUser UserCreated { get; set; }
    }
}