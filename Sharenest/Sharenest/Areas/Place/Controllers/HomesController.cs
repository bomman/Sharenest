using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sharenest.Data;
using Sharenest.Models.BindingModels;
using Sharenest.Models.EntityModels;
using Sharenest.Models.ViewModels.Homes;
using Sharenest.Services.Interfaces;

namespace Sharenest.Areas.Place.Controllers
{
    public class HomesController : Controller
    {
        private readonly IHomesService service;

        public HomesController(IHomesService service)
        {
            this.service = service;
        }

        // GET: Place/Homes
        public ActionResult Index()
        {
            var homes = service.GetLastSixPostedHomes();
            return View(homes);
        }

        // GET: Place/Homes/Details/5
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

        // GET: Place/Homes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Place/Homes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Activities,Provision,Notes,IsVisited,StartDate,EndDate,Rating")] AddHomeBindingModel home)
        {
            if (ModelState.IsValid)
            {
                this.service.AddHome(home);
                return RedirectToAction("Index");
            }

            return View(home);
        }

        // GET: Place/Homes/Edit/5
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

        // POST: Place/Homes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Activities,Provision,Notes,IsVisited,StartDate,EndDate,Rating")] UpdateHomeBindingModel home)
        {
            if (ModelState.IsValid)
            {
                this.service.UpdateHome(home);
                return RedirectToAction("Index");
            }
            return View(home);
        }

        // GET: Place/Homes/Delete/5
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
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            this.service.DeleteHomeById(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.service.Dispose(disposing);
            }
            base.Dispose(disposing);
        }
    }
}
