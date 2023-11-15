using DataAccessLayer.Concrete;
using TraversalCore.CQRS.Commands.DestinationCommands;

namespace TraversalCore.CQRS.Handlers.DestinationHandlers
{
    public class UpdateDestinationCommandHandler
    {
        private readonly Context _context;

        public UpdateDestinationCommandHandler(Context context)
        {
            _context = context;
        }

        public void Handle(UpdateDestinationCommand command)
        {
            var val = _context.Destinations.Find(command.DestinationId);
            val.Price = command.Price;
            val.City = command.City;
            val.Capacity = command.Capacity;
            val.DayNight = command.DayNight;
            _context.Destinations.Update(val);
            _context.SaveChanges();
        }
    }
}
