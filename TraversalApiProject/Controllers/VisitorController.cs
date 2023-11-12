using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TraversalApiProject.DAL.Context;
using TraversalApiProject.DAL.Entities;

namespace TraversalApiProject.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorController : ControllerBase
    {
        [HttpGet]
        public IActionResult VisitorList()
        {
            using (VisitorContext c = new VisitorContext())
            {
                var values = c.Visitors.ToList();
                return Ok(values);
            }
        }
        [HttpGet("{id}")]
        public IActionResult VisitorGetById(int id)
        {
            using (VisitorContext c = new VisitorContext())
            {
                var values = c.Visitors.Where(x => x.VisitorID == id).FirstOrDefault();
                return Ok(values);
            }
        }
        [HttpPost]
        public IActionResult VisitorAdd(Visitor vs)
        {
            using (VisitorContext c = new VisitorContext())
            {
                c.Visitors.Add(vs);
                c.SaveChanges();
                return Ok(vs);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult VisitorDelete(int id)
        {
            using (VisitorContext c = new VisitorContext())
            {
                var values = c.Visitors.Where(x => x.VisitorID == id).FirstOrDefault();
                c.Visitors.Remove(values);
                c.SaveChanges();
                return Ok(values);
            }
        }
        [HttpPut]
        public IActionResult VisitorUpdate(Visitor p)
        {
            using (VisitorContext c = new VisitorContext())
            {
                if (p.VisitorID != 0 && p.VisitorID != null)
                {
                    c.Visitors.Update(p);
                    c.SaveChanges();
                    return Ok(p);
                }
                return BadRequest();
            }
        }
    }
}
