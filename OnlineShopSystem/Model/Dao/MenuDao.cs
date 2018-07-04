using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class MenuDao
    {
        OnlineShopDataContext context = null;
        public MenuDao()
        {
            context = new OnlineShopDataContext();
        }
        public List<Menu> ListByGroupId(int group, string lang)
        {
            return context.Menus.Where(x => x.TypeID == group && x.Status== true && x.Language==lang).OrderBy(x=>x.DisplayOrder).ToList();
        }
        public List<Menu> ListByGroup(int id, string lang)
        {
            return context.Menus.Where(x => x.TypeID == id && x.Status == true && x.Language == lang).OrderBy(x=>x.DisplayOrder).ToList();
        }
    }
}
