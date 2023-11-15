using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TraversalCore.CQRS.Commands.GuideCommands;
using TraversalCore.CQRS.Queries.GuideQueries;

namespace TraversalCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class DestinationMediatRController : Controller
    {
        private readonly IMediator _mediator;

        public DestinationMediatRController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            TempData["adminBreadcrumb"] = "CQRS MediatR ile Rehberler";
            var values = await _mediator.Send(new GetAllGuideQuery());
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> GetGuide(int id)
        {
            var values = await _mediator.Send(new GetGuideByIdQuery(id));
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> AddGuide()
        {
            TempData["adminBreadcrumb"] = "CQRS MediatR ile Yeni Rehber";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddGuide(CreateGuideCommand p)
        {
            await _mediator.Send(p);
            return RedirectToAction("Index", "DestinationMediatR", new { area = "Admin" });
        }
    }
}
