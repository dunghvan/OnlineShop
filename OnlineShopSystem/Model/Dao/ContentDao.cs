using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList;

namespace Model.Dao
{
    public class ContentDao
    {
        OnlineShopDataContext context = null;
        public ContentDao()
        {
            context = new OnlineShopDataContext();
        }
        public Content FindByID(long id)
        {
            var content = context.Contents.Find(id);
            return content;
        }
        public IPagedList<Content> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Content> model = context.Contents;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.MetaTitle.Contains(searchString));
                
            }
            return model.OrderBy(x => x.CreateDate).ToPagedList(page, pageSize);
            
        }
        public bool ChangeStatus(long id)
        {
            var content = context.Contents.Find(id);
            content.Status = !content.Status;
            context.SaveChanges();
            return content.Status;
        }
        public bool Update(Content entity, long _userID) {
            try
            {
                var content = FindByID(entity.ID);
                content.Name = entity.Name;
                content.MetaTitle = entity.MetaTitle;
                content.Description = entity.Description;
                content.Detail = entity.Detail;
                content.Status = entity.Status;
                content.ModifiedDate = DateTime.Now;
                content.ModifiedBy = _userID;
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Boolean Insert(Content entity, long _userID)
        {
            try
            {
                var content = new Content()
                {
                    Name = entity.Name,
                    MetaTitle = entity.MetaTitle,
                    Description = entity.Description,
                    Image = entity.Image,
                    Detail = entity.Detail,
                    CreateDate = DateTime.Now,
                    CreateBy = _userID,
                    Status = true,
                };
                context.Contents.Add(content);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
                


        }
    }
}
