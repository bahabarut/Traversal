namespace TraversalCore.CQRS.Queries.DestinaitonQueries
{
    public class GetDestinationByIdQuery
    {
        public GetDestinationByIdQuery(int Id)
        {
            this.id = Id;
        }
        public int id { get; set; }
    }
}
