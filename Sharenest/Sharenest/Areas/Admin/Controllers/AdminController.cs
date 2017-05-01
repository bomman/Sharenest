using System.Web.Mvc;
using Sharenest.Models.Attributes;

namespace Sharenest.Areas.Admin.Controllers
{
    [Authorize]
    [RoleAuthorization(Roles = "Admin")]
    [RouteArea("Admin")]
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}