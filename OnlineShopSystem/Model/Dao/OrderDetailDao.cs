using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class OrderDetailDao
    {
        OnlineShopDataContext context = null;
        public OrderDetailDao()
        {
            context = new OnlineShopDataContext();
        }
        public bool Insert(OrderDetail detail)
        {
            try {
                context.OrderDetails.Add(detail);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

            

        }
    }
}
