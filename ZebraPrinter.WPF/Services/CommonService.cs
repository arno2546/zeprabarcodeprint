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
            return _ArticleService.GetAll($"api/MasterProduct?CustomerId=2");
        }
    }
}
