using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IReservationService : IGenericService<Reservation>
    {
        List<Reservation> TGetListByFilterWithDestination(Expression<Func<Reservation, bool>> filter);
        List<Reservation> GetResrevationsWithUserDestination();
        Reservation GetReservationWithUserDestination(int id);
    }
}
