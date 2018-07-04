using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList;
namespace Model.Dao
{
    public class SlideDao
    {
        OnlineShopDataContext context = null;
        public SlideDao()
        {
            context = new OnlineShopDataContext();

        }
        public List<Slide> ListAll()
        {
            return context.Slides.ToList();
        }
        public IEnumerable<Slide> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Slide> model = context.Slides.Where(x => x.Status == true);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Description.Contains(searchString));
            }
            
            return model.OrderBy(x => x.CreateDate).ToPagedList(page, pageSize);

        }
        public int Insert(Slide entity)
        {
            entity.Status = true;
            context.Slides.Add(entity);
            context.SaveChanges();
            return entity.ID;
        }
        public Slide FindById(int id)
        {
            return context.Slides.Find(id);
        }
        public bool Delete(int id)
        {
            try
            {
                context.Slides.Remove(FindById(id));
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            

        }
        public bool Update(Slide entity, int user)
        {
            var slide = FindById(entity.ID);
            try
            {
                slide.Image = entity.Image;
                slide.Link = entity.Link;
                slide.Status = entity.Status;
                slide.ModifiedDate = DateTime.Now;
                slide.ModifiedBy = user;
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
