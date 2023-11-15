namespace TraversalCore.CQRS.Results.DestinationResults
{
    public class GetDestinationByIdQueryResult
    {
        public int destinationId { get; set; }
        public string city { get; set; }
        public string daynight { get; set; }
        public int price { get; set; }
        public int capacity { get; set; }
    }
}
