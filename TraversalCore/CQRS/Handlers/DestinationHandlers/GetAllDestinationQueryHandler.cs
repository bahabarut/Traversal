using DataAccessLayer.Concrete;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TraversalCore.CQRS.Results.DestinationResults;

namespace TraversalCore.CQRS.Handlers.DestinationHandlers
{
    public class GetAllDestinationQueryHandler
    {
        private readonly Context _context;

        public GetAllDestinationQueryHandler(Context context)
        {
            _context = context;
        }

        public List<GetAllDestinationQueryResult> Handler()
        {
            var values = _context.Destinations.Select(x => new GetAllDestinationQueryResult()
            {
                id = x.DestinationID,
                city = x.City,
                price = x.Price,
                capacity = x.Capacity,
                daynigth = x.DayNight
            }).AsNoTracking().ToList();

            return values;
        }
    }
}
