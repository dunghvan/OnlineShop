using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class FooterDao
    {
        OnlineShopDataContext context = null;
        public FooterDao()
        {
            context = new OnlineShopDataContext();
        }
        public Footer GetFooter(string lang)
        {
            return context.Footers.SingleOrDefault(x => x.Status == true && x.ID == lang);
        }
    }
}
