using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using Model.ViewModel;

namespace Model.Dao
{
    public class ProductDao
    {
        OnlineShopDataContext context;
        public ProductDao()
        {
            context = new OnlineShopDataContext();
        }
        public IPagedList<Product> GetByName(string key, int page, int pageSize)
        {
            IQueryable<Product> model = context.Products;
                model = model.Where(x => x.Name.Contains(key) || x.Code.Contains(key)).OrderByDescending(x => x.CreateDate);
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }
        public void ViewCount(long id)
        {
            var product = context.Products.Find(id); 
            product.ViewCount += 1;
            context.SaveChanges();
        }
        public List<string> ListName(string keyword)
        {
            return context.Products.Where(x => x.Name.Contains(keyword)).Select(x => x.Name).ToList();
        }
        public IPagedList<Product> ListAllPaging(int page, int pageSize,string searchString)
        {
            IQueryable<Product> model = context.Products;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.MetaTitle.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }
       public IPagedList<Product> ListByCategoryID(long cateID, int page, int pageSize)
        {
            IQueryable<Product> model = context.Products.Where(x=>x.CategoryID == cateID);
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }
        public bool ChangeStatus(long id)
        {
            var product = context.Products.Find(id);
            product.Status = !product.Status;
            context.SaveChanges();
            return product.Status;
        }
        public bool Insert(Product entity, long userID)
        {
            try
            {
                Product product = new Product()
                {
                    Name = entity.Name,
                    Image = entity.Image,
                    Code = entity.Code,
                    MetaTitle = entity.MetaTitle,
                    Description = entity.Description,
                    Quanlity = entity.Quanlity,
                    Detail = entity.Detail,
                    CategoryID = entity.CategoryID,
                    CreateDate = entity.CreateDate,
                    Status = true,
                    CreateBy = userID,
                    Price = entity.Price,
                    Language = entity.Language,
                    Warranty = entity.Warranty,
                    ViewCount = 0
                };
                context.Products.Add(product);
                context.SaveChanges();
                return true;
            }catch(Exception e)
            {
                
                return false;
            }
        }
        public bool Update(Product entity, long userID)
        {
            
            try
            {
                var product = FindbyId(entity.ID);
                product.Image = entity.Image;
                product.MetaTitle = entity.MetaTitle;
                product.Name = entity.Name;
                product.CategoryID = entity.CategoryID;
                product.Code = entity.Code;
                product.Detail = entity.Detail;
                product.Price = entity.Price;
                product.IncludedVAT = entity.IncludedVAT;
                product.PromotionPrice = entity.PromotionPrice;
                product.Warranty = entity.Warranty;
                product.ModifiedDate = DateTime.Now;
                product.ModifiedBy = userID;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool Delete(long id)
        {
            try
            {
                var product = FindbyId(id);
                context.Products.Remove(product);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public List<Product> ListRelatedProduct(long id)
        {
            var product = context.Products.Find(id);
            return context.Products.Where(x => x.ID != id && x.CategoryID == product.CategoryID).Take(4).ToList();
        }
        public List<Product> ListHotProduct(int numOfProduct)
        {
            return context.Products.OrderByDescending(x => x.ViewCount).Take(numOfProduct).ToList();
        }
        public Product ViewDetail(long id)
        {
            return context.Products.Find(id);
        }
        public ProductViewModel ViewDetails(long id) {
            var product = from a in context.Products
                          join b in context.ProductCategories
                          on a.CategoryID equals b.ID
                          join c in context.Users
                          on a.CreateBy equals c.ID
                          where a.ID == id
                          select new ProductViewModel()
                          {
                              ID = a.ID,
                              Name = a.Name,
                              Code = a.Code,
                              MetaTitle = a.MetaTitle,
                              MetaDescriptions = a.MetaDescriptions,
                              MetaKeywords = a.MetaKeywords,
                              Price = a.Price,
                              Description = a.Description,
                              Detail = a.Detail,
                              CreateBy = c.Name,
                              CreateDate = a.CreateDate,
                              ModifiedDate = a.ModifiedDate,
                              ModifiedBy = a.ModifiedBy,
                              Image = a.Image,
                              CategoryName = b.Name,
                              TopHot = a.TopHot,
                              IncludedVAT = a.IncludedVAT,
                              PromotionPrice = a.PromotionPrice,
                              Warranty = a.Warranty,
                              ViewCount = a.ViewCount,
                              Language = a.Language,
                              Quanlity = a.Quanlity
                            };
            return product.Single();

                         

                          
                          

        } 
        public List<Product> ListNewProduct(int top)
        {
            return context.Products.Where(x => x.Status == true).OrderByDescending(x => x.CreateDate).Take(top).ToList();
        }
        public Product FindbyId(long id)
        {
            return context.Products.Find(id);
        }
        public List<Product> ListFeatureProduct(int top)
        {
            return context.Products.Where(x => x.TopHot != null && x.TopHot >= DateTime.Now).OrderByDescending(x => x.CreateDate).Take(top).ToList();
        }
    }
}
