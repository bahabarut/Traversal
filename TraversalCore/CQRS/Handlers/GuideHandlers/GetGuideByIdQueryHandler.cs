﻿using DataAccessLayer.Concrete;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TraversalCore.CQRS.Queries.GuideQueries;
using TraversalCore.CQRS.Results.GuideResults;

namespace TraversalCore.CQRS.Handlers.GuideHandlers
{
    public class GetGuideByIdQueryHandler : IRequestHandler<GetGuideByIdQuery, GetGuideByIdQueryResult>
    {
        private readonly Context _context;

        public GetGuideByIdQueryHandler(Context context)
        {
            _context = context;
        }

        public async Task<GetGuideByIdQueryResult> Handle(GetGuideByIdQuery request, CancellationToken cancellationToken)
        {
            var values = await _context.Guides.FindAsync(request.Id);
            return new GetGuideByIdQueryResult()
            {
                GuideID = values.GuideID,
                Name = values.Name,
                Description = values.Description
            };
        }
    }
}
