using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_EF_SQLSERVE.Models
{
    public class Customer
    {
        [Key]
        [DisplayName("客户")]
        public int CustomerID { get; set; }
        [DisplayName("客户")]
        [Required]
        public string Name { get; set; }
        [DisplayName("联系方式")]
        public string Phone { get; set; }
        [DisplayName("公司名称")]
        public string Company { get; set; }
        [DisplayName("联系地址")]
        public string Address { get; set; }
        [DisplayName("备注")]
        public string Comments { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
    }
}