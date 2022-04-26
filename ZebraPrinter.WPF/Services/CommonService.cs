using posdesktop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZebraPrinter.WPF.Models;

namespace ZebraPrinter.WPF.Services
{
    public class CommonService
    {
        public static IEnumerable<MasterArticle> GetAllMasterArticles()
        {
            CommonHttpService<MasterArticle> _ArticleService = new CommonHttpService<MasterArticle>();
            return _ArticleService.GetAll("api/MasterProductWithPaging?skip=0&take=3&CustomerId=2");
        }

        public static MasterArticle ConvertToMasterArticle(Vw_MasterArticle articleToConvert)
        {
            MasterArticle _MasterArticle = new MasterArticle();
            _MasterArticle.Barcode = articleToConvert.Barcode;
            _MasterArticle.ProductName = articleToConvert.ProductName;
            _MasterArticle.ColorName = articleToConvert.ColorName;
            _MasterArticle.SizeName = articleToConvert.SizeName;
            _MasterArticle.Qty = 1;
            _MasterArticle.RPU = articleToConvert.RPU;
            return _MasterArticle;
        }

    }
}
