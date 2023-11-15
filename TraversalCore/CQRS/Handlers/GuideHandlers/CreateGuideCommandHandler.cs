using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TraversalCore.CQRS.Commands.GuideCommands;

namespace TraversalCore.CQRS.Handlers.GuideHandlers
{
    public class CreateGuideCommandHandler : IRequestHandler<CreateGuideCommand>
    {
        private readonly Context _context;

        public CreateGuideCommandHandler(Context context)
        {
            _context = context;
        }

        //bir tür dönmeyecekse void gibi hareket eden unit MediatR
        public async Task<Unit> Handle(CreateGuideCommand request, CancellationToken cancellationToken)
        {
            Guide gd = new Guide()
            {
                Status = true,
                Name = request.Name,
                Description = request.Desription
            };
            _context.Guides.Add(gd);
            await _context.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
