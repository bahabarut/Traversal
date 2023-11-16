using BusinessLayer.Concrete;
using DataAccessLayer.EntityFreamework;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCore.ViewComponents.Comment
{
    public class CommentList : ViewComponent
    {
        public IViewComponentResult Invoke(int id)
        {
            CommentManager cm = new CommentManager(new EfCommentDal());
            //var values = cm.TGetListByFilter(x => x.DestinationID == id);
            var values = cm.GetListWithAppUserByDestination(id);
            return View(values);

        }
    }
}
