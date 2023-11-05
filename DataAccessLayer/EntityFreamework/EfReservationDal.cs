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
    public class EfReservationDal : GenericRepository<Reservation>, IReservationDal
    {
        public List<Reservation> TGetListByFilterWithDestination(Expression<Func<Reservation, bool>> filter)
        {
            using (Context c = new Context())
            {
                return c.Reservations.Where(filter).Include(x => x.Destination).ToList();
            }
        }
    }
}
