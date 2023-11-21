using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCore.ViewComponents.MemberDashboard
{
    public class Last4Destination : ViewComponent
    {
        private readonly IDestinationService _destinationService;

        public Last4Destination(IDestinationService destinationService)
        {
            _destinationService = destinationService;
        }

        public IViewComponentResult Invoke()
        {
            var values = _destinationService.Last4Destination();
            return View(values);
        }
    }
}
