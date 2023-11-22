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
        public List<Reservation> GetReservationsWithUserDestination()
        {
            using (Context c = new Context())
            {
                return c.Reservations.Include(x => x.Destination).Include(y => y.AppUser).ToList();
            }
        }

        public Reservation GetReservationWithUserDestination(int id)
        {
            using (Context c = new Context())
            {
                return c.Reservations.Where(z=>z.ReservationID == id).Include(x => x.Destination).Include(y => y.AppUser).FirstOrDefault();
            }
        }

        public List<Reservation> TGetListByFilterWithDestination(Expression<Func<Reservation, bool>> filter)
        {
            using (Context c = new Context())
            {
                return c.Reservations.Where(filter).Include(x => x.Destination).Include(y => y.AppUser).ToList();
            }
        }
    }
}
