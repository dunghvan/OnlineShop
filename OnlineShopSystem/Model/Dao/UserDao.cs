using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class UserDao
    {
        OnlineShopDataContext context = null;
        public UserDao()
        {
            context = new OnlineShopDataContext();
        }
        public bool ChangeStatus(long id)
        {
            var user = context.Users.Find(id);
            user.Status = !user.Status;
            context.SaveChanges();
            return user.Status;
        }
        public User GetById(string username)
        {
            return context.Users.SingleOrDefault(x => x.UserName == username);
        }
        public User GetByUserID(long id)
        {
            return context.Users.SingleOrDefault(x => x.ID == id);
        }
        public long Insert(User entity)
        {
            entity.CreateDate = DateTime.Now;
            context.Users.Add(entity);
            context.SaveChanges();
            return entity.ID;
        }
        public bool CheckUserName(string username)
        {
            return context.Users.Count(x => x.UserName == username) > 0;
        }
        public bool CheckEmail(string email)
        {
            return context.Users.Count(x => x.Mail == email) > 0;
        }
        public bool Update(User entity)
        {
            try
            {
                var user = context.Users.Find(entity.ID);
                user.Name = entity.Name;
                user.Phone = entity.Phone;
                user.Mail = entity.Mail;
                user.ModifiedBy = entity.ID;
                user.ModifiedDate = DateTime.Now;
                context.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }
        public User ViewDetail(int id)
        {
            return context.Users.Find(id);
        }
        public IEnumerable<User> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<User> model = context.Users;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.UserName.Contains(searchString) || x.Name.Contains(searchString));
            }
            return model.OrderBy(x=>x.CreateDate).ToPagedList( page, pageSize);
        }
        public int Login(string username, string password)
        {
            var result = context.Users.SingleOrDefault(x => x.UserName == username);
            if (result == null)
            {
                return 0;
            }
            else
            {
               if(result.Status == false)
                {
                    return -1;
                }else if(result.Password == password)
                {
                    return 1;
                }
                else
                {
                    return -2;
                }
            }
        }
        public bool Delete(int id)
        {
            try
            {
                var user = context.Users.Find(id);
                context.Users.Remove(user);
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
