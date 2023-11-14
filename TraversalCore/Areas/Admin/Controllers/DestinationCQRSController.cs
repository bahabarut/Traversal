using Microsoft.AspNetCore.Mvc;
using TraversalCore.CQRS.Handlers.DestinationHandlers;
using TraversalCore.CQRS.Queries.DestinaitonQueries;

namespace TraversalCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DestinationCQRSController : Controller
    {
        private readonly GetAllDestinationQueryHandler _getAllDestinationQueryHandler;
        private readonly GetDestinationByIdQueryHandler _getDestinationByIdQueryHandler;

        public DestinationCQRSController(GetAllDestinationQueryHandler getAllDestinationQueryHandler, GetDestinationByIdQueryHandler getDestinationByIdQueryHandler)
        {
            _getAllDestinationQueryHandler = getAllDestinationQueryHandler;
            _getDestinationByIdQueryHandler = getDestinationByIdQueryHandler;
        }

        public IActionResult Index()
        {
            TempData["adminBreadcrumb"] = "CQRS ile Tur Verileri";
            var values = _getAllDestinationQueryHandler.Handler();
            return View(values);
        }
        [HttpGet]
        public IActionResult GetDestination(int id)
        {
            TempData["adminBreadcrumb"] = "CQRS ile Tur Verileri Düzenleme-" + id;
            var values = _getDestinationByIdQueryHandler.Handle(new GetDestinationByIdQuery(id));
            return View(values);
        }

        //[HttpPost]
        //public IActionResult GetDestination(int id)
        //{
        //    TempData["adminBreadcrumb"] = "CQRS ile Tur Verileri Düzenleme-" + id;
        //    var values = _getDestinationByIdQueryHandler.Handle(new GetDestinationByIdQuery(id));
        //    return View(values);
        //}
    }
}
