using DataAccessLayer.Concrete;
using System.Linq;
using TraversalCore.CQRS.Queries.DestinaitonQueries;
using TraversalCore.CQRS.Results.DestinationResults;

namespace TraversalCore.CQRS.Handlers.DestinationHandlers
{
    public class GetDestinationByIdQueryHandler
    {
        private readonly Context _context;

        public GetDestinationByIdQueryHandler(Context context)
        {
            _context = context;
        }

        public GetDestinationByIdQueryResult Handle(GetDestinationByIdQuery query)
        {
            var value = _context.Destinations.Find(query.id);
            return new GetDestinationByIdQueryResult
            {
                destinationId = value.DestinationID,
                city = value.City,
                daynight = value.DayNight,
                price = value.Price
            };
        }
    }
}
