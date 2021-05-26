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
    public class MovieModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MovieModels
        public ActionResult Index()
        {
            return View(db.MovieModels.ToList());
        }

        // GET: MovieModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieModel movieModel = db.MovieModels.Find(id);
            if (movieModel == null)
            {
                return HttpNotFound();
            }
            return View(movieModel);
        }

        // GET: MovieModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MovieModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,GenreId,Image,Price")] MovieModel movieModel)
        {
            if (ModelState.IsValid)
            {

              
                          
                db.MovieModels.Add(movieModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movieModel);
        }

        // GET: MovieModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieModel movieModel = db.MovieModels.Find(id);
            if (movieModel == null)
            {
                return HttpNotFound();
            }
            return View(movieModel);
        }

        // POST: MovieModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,GenreId,Image,Price")] MovieModel movieModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movieModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movieModel);
        }

        // GET: MovieModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieModel movieModel = db.MovieModels.Find(id);
            if (movieModel == null)
            {
                return HttpNotFound();
            }
            return View(movieModel);
        }

        // POST: MovieModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MovieModel movieModel = db.MovieModels.Find(id);
            db.MovieModels.Remove(movieModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Genre/Details/5
        public GenreModel GetGenreDetails(int? id)
        {
            if (id == null)
            {
                return null;
            }
            GenreModel genreModel = db.GenreModels.Find(id);
            if (genreModel == null)
            {
                return null;
            }
           return genreModel ;
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
