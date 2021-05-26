using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cinema.Web.Models;

namespace Cinema.Web.Controllers
{
    public class GenreController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Genre
        public ActionResult Index()
        {
            return View(db.GenreModels.ToList());
        }

        // GET: Genre/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GenreModel genreModel = db.GenreModels.Find(id);
            if (genreModel == null)
            {
                return HttpNotFound();
            }
            return View(genreModel);
        }

        // GET: Genre/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Genre/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] GenreModel genreModel)
        {
            if (ModelState.IsValid)
            {
                db.GenreModels.Add(genreModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(genreModel);
        }

        // GET: Genre/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GenreModel genreModel = db.GenreModels.Find(id);
            if (genreModel == null)
            {
                return HttpNotFound();
            }
            return View(genreModel);
        }

        // POST: Genre/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] GenreModel genreModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(genreModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(genreModel);
        }

        // GET: Genre/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GenreModel genreModel = db.GenreModels.Find(id);
            if (genreModel == null)
            {
                return HttpNotFound();
            }
            return View(genreModel);
        }

        // POST: Genre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GenreModel genreModel = db.GenreModels.Find(id);
            db.GenreModels.Remove(genreModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
