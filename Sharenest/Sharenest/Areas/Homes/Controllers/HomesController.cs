using System;
using System.Net;
using System.Web.Mvc;
using Sharenest.Models.BindingModels;
using Sharenest.Models.ViewModels.Homes;
using Sharenest.Services.Interfaces;

namespace Sharenest.Areas.Homes.Controllers
{
    [Authorize]
    [RouteArea("Homes")]
    public class HomesController : Controller
    {
        private readonly IHomesService service;

        public HomesController(IHomesService service)
        {
            this.service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route]
        [Route("Index")]
        public ActionResult Index()
        {
            var homes = service.GetLastSixPostedHomes();
            return View(homes);
        }

        [HttpGet]
        [Route("Details/{id}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HomeDetailsViewModel home = this.service.GetHomeDetailsViewModelById((int) id);
            if (home == null)
            {
                return HttpNotFound();
            }
            return View(home);
        }

        [HttpGet]
        [Route("Create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public ActionResult Create([Bind(Include = "Id,Name,Location.Country,Location.Name,Activities,Provision,Notes,StartDate,EndDate")] AddHomeBindingModel home)
        {
            if (ModelState.IsValid)
            {
                this.service.AddHome(home);
                return RedirectToAction("Index");
            }

            return View(home);
        }

        [HttpGet]
        [Route("Edit/{id}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HomeEditViewModel home = this.service.GetHomeEditViewModelById((int) id);

            if (home == null)
            {
                return HttpNotFound();
            }
            return View(home);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Location.Country,Location.Name,Activities,Provision,Notes,StartDate,EndDate")] UpdateHomeBindingModel home)
        {
            if (ModelState.IsValid)
            {
                this.service.UpdateHome(home);
                return RedirectToAction("Index");
            }

            var viewModel = this.service.ChangeUpdateHomeBindingModelToHomesEditViewModel(home);
            
            return View(viewModel);
        }

        [HttpGet]
        [Route("Delete/{id}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomeDetailsViewModel home = this.service.GetHomeDetailsViewModelById((int)id);
            if (home == null)
            {
                return HttpNotFound();
            }
            return View(home);
        }

        // POST: Place/Homes/Delete/5
        [HttpPost, ActionName("Delete")]
        [Route("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            this.service.DeleteHomeById(id);
            return RedirectToAction("Index");
        }
    }
}
