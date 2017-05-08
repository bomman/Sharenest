using System.Collections.Generic;
using System.Web.Mvc;
using Sharenest.Models.Attributes;
using Sharenest.Models.ViewModels.Admin;
using Sharenest.Services.Interfaces;

namespace Sharenest.Areas.Admin.Controllers
{
    [Authorize]
    [RoleAuthorization(Roles = "Admin")]
    [RouteArea("Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService service;

        public AdminController(IAdminService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route]
        [Route("Index")]
        public ActionResult Index()
        {

            return View();
        }

        [HttpGet]
        [Route("Persons")]
        public ActionResult Persons()
        {
            IEnumerable<AdminPersonsViewModel> viewModels = this.service.GetAllPerons();
            return View(viewModels);
        }

        [HttpGet]
        [Route("Homes")]
        public ActionResult Homes()
        {
            IEnumerable<AdminHomesViewModel> viewModels = this.service.GetAllHomes();

            return View(viewModels);
        }
    }
}