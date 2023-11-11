using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFreamework
{
    public class EfContactUsDal : GenericRepository<ContactUs>, IContactUsDal
    {
        public void ChangeToFalse(int id)
        {
            using (Context c = new Context())
            {
                var value = c.ContactUses.Find(id);
                value.MessageStatus = false;
                c.SaveChanges();
            }
        }
    }
}
