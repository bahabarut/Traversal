using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using TraversalCore.CQRS.Commands.DestinationCommands;

namespace TraversalCore.CQRS.Handlers.DestinationHandlers
{
    public class CreateDestinationCommanHandler
    {
        private readonly Context _context;

        public CreateDestinationCommanHandler(Context context)
        {
            _context = context;
        }
        public void Handle(CreateDestinationCommand command)
        {
            _context.Destinations.Add(new Destination()
            {
                City = command.City,
                Price = command.Price,
                DayNight = command.DayNight,
                Capacity = command.Capacity,
                Status = true
            });
            _context.SaveChanges();
        }
    }
}
