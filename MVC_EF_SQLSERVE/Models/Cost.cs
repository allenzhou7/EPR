using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_EF_SQLSERVE.Models
{
    public class Cost
    {
        [Key]
        public int CostID { get; set; }
        [DisplayName("进货日期")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CostDate { get; set; }
        [DisplayName("进货材料")]
        [Required]
        public string CostDesc { get; set; }
        [DisplayName("数量")]
        [Required]
        public int Count { get; set; }
        [DisplayName("单价")]
        [Required]
        public decimal Price { get; set; }
        [DisplayName("金额")]
        [Required]
        public decimal Amount { get; set; }
    }
}