using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICommentService : IGenericService<Comment>
    {
        List<Comment> GetListWithDestination();
        List<Comment> TGetListByFilterWithDestination(Expression<Func<Comment, bool>> filter);
    }
}
