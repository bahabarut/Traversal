using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFreamework
{
    public class EfCommentDal : GenericRepository<Comment>, ICommentDal
    {
        public List<Comment> GetListWithAppUserByDestination(int id)
        {
            using (Context c = new Context())
            {
                return c.Comments.Include(x => x.AppUser).Where(y => y.DestinationID == id).ToList();
            }
        }

        public List<Comment> GetListWithDestination()
        {
            using (Context c = new Context())
            {
                return c.Comments.Include(x => x.Destination).Include(y => y.AppUser).ToList();
            }
        }

        public List<Comment> TGetListByFilterWithDestination(Expression<Func<Comment, bool>> filter)
        {
            using (Context c = new Context())
            {
                return c.Comments.Include(x => x.Destination).Where(filter).ToList();
            }
        }
    }
}
