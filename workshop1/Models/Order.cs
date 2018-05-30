using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace workshop1.Models
{
    public class Order
    {
        /// <summary>
        /// 訂單詳細資料
        /// </summary>
        public List<OrderDetail> Details { get; set; }

        /// <summary>
        /// 訂單ID
        /// </summary>
        public int OrderID { get; set; }

        [Display(Name="客戶名稱")]
        [Required]
        public int CustomerID { get; set; }

        [Display(Name = "負責員工名稱")]
        [Required]
        public int EmployeeID { get; set; }

        [Display(Name ="訂單日期")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime OrderDate { get; set; }

        [Display(Name = "需要日期")]
        [Required]
        public DateTime RequiredDate { get; set; }

        [Display(Name = "出貨日期")]
        public DateTime? ShippedDate { get; set; }

        [Display(Name = "出貨公司")]
        [Required]
        public int? ShipperID { get; set; }

        [Display(Name = "運費")]
        [Required]
        public decimal? Freight { get; set; }

        [Display(Name = "出貨國家")]
        [MaxLength(15, ErrorMessage = "字數長度不可超過{1}")]
        [Required]
        public string ShipCountry { get; set; }

        [Display(Name = "出貨城市")]
        [MaxLength(15, ErrorMessage = "字數長度不可超過{1}")]
        [Required]
        public string ShipCity { get; set; }

        [Display(Name = "出貨地區")]
        [MaxLength(15, ErrorMessage = "字數長度不可超過{1}")]
        public string ShipRegion { get; set; }

        [Display(Name = "郵遞區號")]
        [MaxLength(10, ErrorMessage = "字數長度不可超過{1}")]
        public string ShipPostalCode { get; set; }

        [Display(Name = "出貨地址")]
        [MaxLength(60, ErrorMessage ="字數長度不可超過{1}")]
        [Required]
        public string ShipAddress { get; set; }
    }
}