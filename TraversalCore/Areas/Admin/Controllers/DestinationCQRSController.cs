using Microsoft.AspNetCore.Mvc;
using TraversalCore.CQRS.Commands.DestinationCommands;
using TraversalCore.CQRS.Handlers.DestinationHandlers;
using TraversalCore.CQRS.Queries.DestinaitonQueries;

namespace TraversalCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class DestinationCQRSController : Controller
    {
        private readonly GetAllDestinationQueryHandler _getAllDestinationQueryHandler;
        private readonly GetDestinationByIdQueryHandler _getDestinationByIdQueryHandler;
        private readonly CreateDestinationCommanHandler _createDestinationHandler;
        private readonly DeleteDestinationCommandHandler _deleteDestinationHandler;
        private readonly UpdateDestinationCommandHandler _updateDestinationHandler;


        public DestinationCQRSController(GetAllDestinationQueryHandler getAllDestinationQueryHandler, GetDestinationByIdQueryHandler getDestinationByIdQueryHandler, CreateDestinationCommanHandler createDestinationHandler, DeleteDestinationCommandHandler deleteDestinationHandler, UpdateDestinationCommandHandler updateDestinationHandler)
        {
            _getAllDestinationQueryHandler = getAllDestinationQueryHandler;
            _getDestinationByIdQueryHandler = getDestinationByIdQueryHandler;
            _createDestinationHandler = createDestinationHandler;
            _deleteDestinationHandler = deleteDestinationHandler;
            _updateDestinationHandler = updateDestinationHandler;
        }

        public IActionResult Index()
        {
            TempData["adminBreadcrumb"] = "CQRS ile Tur Verileri";
            var values = _getAllDestinationQueryHandler.Handler();
            return View(values);
        }

        [HttpGet]
        public IActionResult AddDestination()
        {
            TempData["adminBreadcrumb"] = "CQRS ile Tur Ekleme";
            return View();
        }

        [HttpPost]
        public IActionResult AddDestination(CreateDestinationCommand p)
        {
            _createDestinationHandler.Handle(p);
            return RedirectToAction("Index", "DestinationCQRS", new { area = "Admin" });
        }
        public IActionResult DeleteDestination(int id)
        {
            _deleteDestinationHandler.Handle(new DeleteDestinationCommand(id));
            return RedirectToAction("Index", "DestinationCQRS", new { area = "Admin" });
        }
        [HttpGet]
        public IActionResult UpdateDestination(int id)
        {
            TempData["adminBreadcrumb"] = "CQRS ile Tur Verileri Düzenleme-" + id;
            var values = _getDestinationByIdQueryHandler.Handle(new GetDestinationByIdQuery(id));
            UpdateDestinationCommand dt = new UpdateDestinationCommand()
            {
                DestinationId = values.destinationId,
                City = values.city,
                DayNight = values.daynight,
                Price = values.price,
                Capacity = values.capacity
            };
            return View(dt);
        }
        [HttpPost]
        public IActionResult UpdateDestination(UpdateDestinationCommand p)
        {
            _updateDestinationHandler.Handle(p);
            return RedirectToAction("Index", "DestinationCQRS", new { area = "Admin" });
        }
    }
}
