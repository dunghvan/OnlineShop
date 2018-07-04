using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
namespace Model.Dao
{
    public class ContactDao
    {
        OnlineShopDataContext contex = null;
        public ContactDao()
        {
            contex = new OnlineShopDataContext();
        }
        public List<Contact> ListAll() {
            return contex.Contacts.ToList();
        }
        public Contact GetToShow()
        {
            return contex.Contacts.FirstOrDefault();
        }
        public int Insert(Contact entity)
        {
                contex.Contacts.Add(entity);
                contex.SaveChanges();
                return entity.ID;
        }
        public Contact FindByID(int id)
        {
            return contex.Contacts.Find(id);
        }
        public bool ChangeStatus( int id)
        {
            var contact = FindByID(id);
            contact.Status = !contact.Status;
            contex.SaveChanges();
            return contact.Status;
        }
    }
}
