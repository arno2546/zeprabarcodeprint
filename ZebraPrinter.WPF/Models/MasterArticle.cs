using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZebraPrinter.WPF.Models
{
    public class MasterArticle
    {
        public string ProductName { get; set; }
        public string ColorName { get; set; }
        public string SizeName { get; set; }
        public string Barcode { get; set; }
        public decimal RPU { get; set; }
        public int SkuId { get; set; }
        public int ProductId { get; set; }
        public string Sku { get; set; }
        public int? BuId { get; set; }
        public int? ProdDetailsID { get; set; }
        public decimal CPU { get; set; }
        public bool IsQc { get; set; }
        public int ServerPkId { get; set; }
        public decimal VatPercent { get; set; }
        public decimal DiscRatio { get; set; }
        public string ModelName { get; set; }
        public string BrandName { get; set; }
        public string GroupName { get; set; }
        public int Qty { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsApplyDiscValue { get; set; }
        public decimal DiscountedPrice { get; set; }
        public bool ManualDisc { get; set; } = false;
        public bool IsBoga { get; set; }
        public decimal VatValue { get; set; }
        //public bool IsReturn { get; set; } = false;
        //public int MaxReturnQty { get; set; } = 0;
        //public DateTime CreatedDate { get; set; }
        //public DateTime? UpdatedDate { get; set; }
    }
}
