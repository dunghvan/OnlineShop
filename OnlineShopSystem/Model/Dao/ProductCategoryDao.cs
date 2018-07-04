using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.Dao
{

    public class ProductCategoryDao
    {
        OnlineShopDataContext context = null;
        public ProductCategoryDao()
        {
            context = new OnlineShopDataContext();
        }
        public List<ProductCategory> ListAll(string lang)
        {
            
            return context.ProductCategories.Where(x => x.Status == true && x.Language == lang).OrderBy(x => x.DisplayOrder).ToList();
        }
        public ProductCategory ViewDetail(long id)
        {
            return context.ProductCategories.Find(id);

        }
    }
}
