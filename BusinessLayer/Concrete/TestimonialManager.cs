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
    public class TestimonialManager : ITestimonialService
    {
        ITestimonialDal _testmionalDal;

        public TestimonialManager(ITestimonialDal testmionalDal)
        {
            _testmionalDal = testmionalDal;
        }

        public void TAdd(Testimonial t)
        {
            _testmionalDal.Insert(t);
        }

        public void TDelete(Testimonial t)
        {
            _testmionalDal.Delete(t);
        }

        public Testimonial TGetById(int id)
        {
            return _testmionalDal.GetById(id);
        }

        public List<Testimonial> TGetList()
        {
            return _testmionalDal.GetList();
        }

        public List<Testimonial> TGetListByFilter(Expression<Func<Testimonial, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(Testimonial t)
        {
            _testmionalDal.Update(t);
        }
    }
}
