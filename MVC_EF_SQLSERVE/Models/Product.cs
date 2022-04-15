using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_EF_SQLSERVE.Models
{
    public class Product
    {
        [Key]
        [DisplayName("产品")]
        public int ProductID { get; set; }
        [Required]
        [DisplayName("品名")]
        [StringLength(30, MinimumLength = 1)]
        public string Name { get; set; }
        [Required]
        [DisplayName("规格")]
        public string Length { get; set; }
        [Required]
        [DisplayName("颜色")]
        public string Colour { get; set; }
        [DisplayName("产品名称")]
        public string FullName { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
    }
}