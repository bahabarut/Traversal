﻿using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IReservationDal : IGenericDal<Reservation>
    {
        List<Reservation> TGetListByFilterWithDestination(Expression<Func<Reservation, bool>> filter);
        List<Reservation> GetReservationsWithUserDestination();
        Reservation GetReservationWithUserDestination(int id);
    }
}
