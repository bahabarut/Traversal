using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ICommentDal : IGenericDal<Comment>
    {
        public List<Comment> GetListWithDestination();
        public List<Comment> GetListWithAppUserByDestination(int id);
        public List<Comment> TGetListByFilterWithDestination(Expression<Func<Comment, bool>> filter);
    }
}
