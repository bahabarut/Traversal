using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFreamework
{
    public class EfDestinaitonDal : GenericRepository<Destination>, IDestinationDal
    {
        public Destination GetDestinationByIdWithGuide(int id)
        {
            using (Context c = new Context())
            {
                return c.Destinations.Include(x => x.Guide).Where(y => y.DestinationID == id).FirstOrDefault();
            }
        }

        public List<Destination> Last4Destination()
        {
            using (Context c = new Context())
            {
                return c.Destinations.OrderByDescending(x => x.DestinationID).Take(4).ToList();
            }
        }
    }
}
