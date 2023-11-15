using MediatR;
using System.Collections.Generic;
using TraversalCore.CQRS.Results.GuideResults;

namespace TraversalCore.CQRS.Queries.GuideQueries
{
    public class GetAllGuideQuery : IRequest<List<GetAllGuideQueryResult>>
    {
    }
}
