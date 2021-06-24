using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cinema.Web.Models;
using Cinema.Web.Services;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Drawing;
using System.IO;
using Syncfusion.Pdf.Grid;

namespace Cinema.Web.Controllers
{
    public class MovieModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private MovieService moviesService = new MovieService();

        // GET: MovieModels
        public ActionResult Index()
        {
            var movies = moviesService.GetAllMovies();
            ViewBag.Movies = movies;

            var genres = moviesService.GetAllGenres();

            List<SelectListItem> listItems = new List<SelectListItem>();

            foreach (var item in genres)
            {
                listItems.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                });
            }

            ViewBag.Genres = genres;

            ViewBag.ListItems = listItems;

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

        public ActionResult AddTicketToShoppingCart(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCart shoppingModel = null;
            if (Session["user_id"] != null)
            {
               shoppingModel = moviesService.AddTicketToShoppingCart(id, Session["user_id"].ToString());

            }
            
            
            if (shoppingModel == null)
            {
                Response.Redirect("/Account/Login", true);
                return new EmptyResult();
            }
            else
            {
                Response.Redirect("/MovieModels/ShoppingCart/" + Session["cart_id"].ToString(), true);
                return new EmptyResult();
            }
            
        }

        public ActionResult ShoppingCart()
        {
            try
            {
                var shoppingCart = db.ShoppingCarts.Find((int)Session["cart_id"]);
                return View(shoppingCart);
            }
            catch (Exception ex)
            {

                Response.Redirect("/Account/Login");
                return new EmptyResult();
            }
        }

        public ActionResult GenerateReport()
        {
            try
            {
                var tickets = moviesService.GetAllPayedTicketsForUser(Session["user_id"].ToString());


                PdfDocument doc = new PdfDocument();
                PdfPage page = doc.Pages.Add();
                PdfGrid pdfGrid = new PdfGrid();

                List<object> data = new List<object>();

                foreach(var item in tickets)
                {
                    Object row1 = new { Movie = item.Movie.Name, Price = "$" + item.Price, Amount = item.NumberOfTickets ,Total = "$" + item.Price * item.NumberOfTickets, Date = item.DateOfReservation };
                    data.Add(row1);
                }

                IEnumerable<object> dataTable = data;
                pdfGrid.DataSource = dataTable;
                pdfGrid.Draw(page, new Syncfusion.Drawing.PointF(10, 10));
                MemoryStream stream = new MemoryStream();
                doc.Save(stream);
                stream.Position = 0;
                doc.Close(true);
                string contentType = "application/pdf";
                string fileName = "Report-" + DateTime.Now.ToString("dd.MM.yyyy") + ".pdf";
                return File(stream, contentType, fileName);

            }
            catch (Exception ex)
            {

                return new EmptyResult();
            }
        }

        public ActionResult IncrementTicket(int ticketId)
        {
            try
            {
                var shoppingCart = db.ShoppingCarts.Find((int)Session["cart_id"]);

                moviesService.IncrementTicket(ticketId);
                Response.Redirect("/MovieModels/ShoppingCart/" + Session["cart_id"].ToString(), true);
                return new EmptyResult();

            }
            catch (Exception ex)
            {

                Response.Redirect("/Account/Login");
                return new EmptyResult();
            }
        }

        public ActionResult DecrementTicket(int ticketId)
        {
            try
            {
                var shoppingCart = db.ShoppingCarts.Find((int)Session["cart_id"]);

                moviesService.DecrementTicket(ticketId);
                Response.Redirect("/MovieModels/ShoppingCart/" + Session["cart_id"].ToString(), true);
                return new EmptyResult();
            }
            catch (Exception ex)
            {

                Response.Redirect("/Account/Login");
                return new EmptyResult();
            }
        }

        public ActionResult RemoveTicket(int ticketId)
        {
            try
            {
                var shoppingCart = db.ShoppingCarts.Find((int)Session["cart_id"]);

                moviesService.DeleteTicket(ticketId);
                Response.Redirect("/MovieModels/ShoppingCart/" + Session["cart_id"].ToString(), true);
                return new EmptyResult();
            }
            catch (Exception ex)
            {

                Response.Redirect("/Account/Login");
                return new EmptyResult();
            }
        }


        public ActionResult PayForTickets(int cartId)
        {
            moviesService.PayForTickets(cartId);

            //var ticketsModel = moviesService.GetAllPayedTicketsForUser(Session["user_id"].ToString());
            //ViewBag.PayedTickets = ticketsModel;

            Response.Redirect("/MovieModels/Tickets/" + Session["user_id"]);
            return new EmptyResult();
        }

        public ActionResult Tickets()
        {

            var ticketsModel = moviesService.GetAllPayedTicketsForUser(Session["user_id"].ToString());
           

            return View(ticketsModel);
        }



        // GET: MovieModels/Create
        public ActionResult Create()
        {
            var genres = moviesService.GetAllGenres();

            List<SelectListItem> listItems = new List<SelectListItem>();

            foreach (var item in genres)
            {
                listItems.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                });
            }

            ViewBag.Genres = genres;
            ViewBag.ListItems = listItems;

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

            var genres = moviesService.GetAllGenres();

            List<SelectListItem> listItems = new List<SelectListItem>();

            foreach (var item in genres)
            {
                listItems.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                });
            }

            ViewBag.Genres = genres;

            ViewBag.ListItems = listItems;
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
