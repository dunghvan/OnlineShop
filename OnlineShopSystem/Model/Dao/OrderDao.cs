using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class OrderDao
    {
         OnlineShopDataContext context = null;
        public OrderDao()
        {
            context = new OnlineShopDataContext();
        }
          
        public IPagedList<Order> ListAllPaging(int page, int pageSize,string searchString)
        {
            IQueryable<Order> model = context.Orders;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.ShipName.Contains(searchString) || x.ShipEmail.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }
        public bool Check(long id)
        {
            try
            {
                var order = context.Orders.Find(id);
                order.Status = 1;
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }
        public long Insert(Order order)
        {
            context.Orders.Add(order);
            context.SaveChanges();
            return order.ID;

        }
        public int Unconfirmed()
        {
            return context.Orders.Where(x => x.Status == null).Count();
        }
    }
}
