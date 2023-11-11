using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ContactUsManager : IContactUsService
    {
        IContactUsDal _contactUsDal;

        public ContactUsManager(IContactUsDal contactUsDal)
        {
            _contactUsDal = contactUsDal;
        }

        public void ChangeToFalse(int id)
        {
            _contactUsDal.ChangeToFalse(id);
        }

        public void TAdd(ContactUs t)
        {
            _contactUsDal.Insert(t);
        }

        public void TDelete(ContactUs t)
        {
            _contactUsDal.Delete(t);
        }

        public ContactUs TGetById(int id)
        {
            return _contactUsDal.GetById(id);
        }

        public List<ContactUs> TGetList()
        {
            return _contactUsDal.GetList();
        }

        public List<ContactUs> TGetListByFilter(Expression<Func<ContactUs, bool>> filter)
        {
            return _contactUsDal.GetListByFilter(filter);
        }

        public void TUpdate(ContactUs t)
        {
            _contactUsDal.Update(t);
        }
    }
}
