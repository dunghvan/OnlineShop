using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class CategoryDao
    {
        OnlineShopDataContext context = null;
        public CategoryDao()
        {
            context = new OnlineShopDataContext();
        }
        public IEnumerable<Category> ListAll()
        {
            var dao = new CategoryDao();
            return dao.context.Categories.Where(x => x.Status == true);
        }
        public IEnumerable<Category> ListAll(string searchString, int page, int pageSize)
        {
            IQueryable<Category> cate = context.Categories;
            if (!string.IsNullOrEmpty(searchString))
            {
                cate = cate.Where(x => x.Name.Contains(searchString));
            }
            return cate.OrderBy(x=>x.CreateDate).ToPagedList( page, pageSize);
        }
        public long Insert(Category cate)
        {
            context.Categories.Add(cate);
            context.SaveChanges();
            return cate.ID;
        }
        public Category FindByID(long id)
        {
            return context.Categories.Find(id);
        }
        public ProductCategory ViewDetail(long id)
        {
            return context.ProductCategories.Find(id);

        }

        public bool Update(Category entity)
        {
            try
            {
                var cate = FindByID(entity.ID);
                cate.Name = entity.Name;
                cate.Status = entity.Status;
                cate.ModifiedDate = DateTime.Now;
                context.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }
        public bool Delete(long id)
        {
            try
            {
                var cate = FindByID(id);
                context.Categories.Remove(cate);
                context.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }
    }
}
