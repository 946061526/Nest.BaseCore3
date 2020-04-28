using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Nest.BaseCore.Domain.Entity
{
    [Table("tbStock")]
    public class Stock
    {
        [Key]
        public string id { get; set; }
        public string storeHouse { get; set; }
        public string code { get; set; }
        public string goodsCode { get; set; }
        public string color { get; set; }
        //public string size { get; set; }
        public string price { get; set; }
        public int stockNum { get; set; }
        public string stockMoney { get; set; }
        public string styleNo { get; set; }
        public string asi { get; set; }
    }

    [Table("tbStockCheck")]
    public class StockCheck
    {
        [Key]
        public string id { get; set; }
        public string code { get; set; }
        public string type { get; set; }
        public string price { get; set; }
        public string styleNo { get; set; }
        public string name { get; set; }
        public string color { get; set; }
        public string size { get; set; }
        public int num { get; set; }
        public int stockNum { get; set; } = 0;
        public string goodsNo { get; set; }
        public string asi { get; set; }
    }
}
