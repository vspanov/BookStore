using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
using BookStore.Util;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        // создаем контекст данных
        BookContext db = new BookContext();

        public ActionResult Index()
        {
            return View(db.Books);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        //public ActionResult Index()
        //{
        //    // получаем из бд все объекты Book
        //    IEnumerable<Book> books = db.Books;
        //    // передаем все объекты в динамическое свойство Books в ViewBag
        //    ViewBag.Books = books;
        //    // возвращаем представление
        //    return View();
        //}

        [HttpGet]
        public ActionResult Buy(int id)
        {
            ViewBag.BookId = id;
            return View();
        }
        [HttpPost]
        public string Buy(Purchase purchase)
        {
            purchase.Date = DateTime.Now;
            // добавляем информацию о покупке в базу данных
            db.Purchases.Add(purchase);
            // сохраняем в бд все изменения
            db.SaveChanges();
            return "Спасибо, " + purchase.Person + ", за покупку!";
        }

        [HttpGet]
        public ActionResult EditBook(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Book book = db.Books.Find(id);
            if (book != null)
            {
                return View(book);
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult EditBook(Book book)
        {
            db.Entry(book).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Book book)
        {
            db.Books.Add(book);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Book b = db.Books.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            return View(b);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Book b = db.Books.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            db.Books.Remove(b);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //public ActionResult Delete(int id)
        //{
        //    Book b = db.Books.Find(id);
        //    if (b != null)
        //    {
        //        db.Books.Remove(b);
        //        db.SaveChanges();
        //    }
        //    return RedirectToAction("Index");
        //}

        //public ActionResult Delete(int id)
        //{
        //    Book b = new Book { Id = id };
        //    db.Entry(b).State = EntityState.Deleted;
        //    db.SaveChanges();

        //    return RedirectToAction("Index");
        //}
        //public ActionResult GetHtml()
        //{
        //    return new HtmlResult("<h2>Привет мир!</h2>");
        //}

        //public ActionResult GetImage()
        //{
        //    string path = "../Images/visualstudio.png";
        //    return new ImageResult(path);
        //}

        //public ActionResult Partial()
        //{
        //    ViewBag.Message = "Это частичное представление.";
        //    return PartialView();
        //}
    }
}