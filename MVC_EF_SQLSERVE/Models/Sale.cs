using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_EF_SQLSERVE.Models
{
    public class Sale
    {
        [Key]
        public int SaleID { get; set; }
        [DisplayName("日期")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime SaleDate { get; set; }
        [DisplayName("件数")]
        public int? Number { get; set; }
        [DisplayName("数量")]
        [Required]
        public int Count { get; set; }
        [DisplayName("单价")]
        [Required]
        public decimal Price { get; set; }
        [DisplayName("金额")]
        [Required]
        public decimal Amount { get; set; }
        [DisplayName("备注")]
        public string Comments { get; set; }
        [DisplayName("客户")]
        [ForeignKey("Customer")]
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
        [DisplayName("产品")]
        [ForeignKey("Product")]
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }
    }
}