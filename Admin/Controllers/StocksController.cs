using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL.Context;
using DAL.Model;

namespace Admin.Controllers
{
    [Authorize]
    public class StocksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Stocks
        public ActionResult Index()
        {
            var stocks = db.Stocks.Include(s => s.Product);
            return View(stocks.ToList());
        }

        // GET: Stocks/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // GET: Stocks/Create
        public ActionResult Create()
        {
            ViewBag.MachineID = new SelectList(db.Machines, "MachineID", "Code");
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "MarcucciCode");
            return View();
        }

        // POST: Stocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StockID,MachineKeyBoardNumber,Quantity,MachineID,ProductID")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                stock.StockID = Guid.NewGuid();
                db.Stocks.Add(stock);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MachineID = new SelectList(db.Machines, "MachineID", "Code", stock.MachineID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "MarcucciCode", stock.ProductID);
            return View(stock);
        }

        // GET: Stocks/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            ViewBag.MachineID = new SelectList(db.Machines, "MachineID", "Code", stock.MachineID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "MarcucciCode", stock.ProductID);
            return View(stock);
        }

        // POST: Stocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StockID,MachineKeyBoardNumber,Quantity,MachineID,ProductID")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MachineID = new SelectList(db.Machines, "MachineID", "Code", stock.MachineID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "MarcucciCode", stock.ProductID);
            return View(stock);
        }

        // GET: Stocks/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // POST: Stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Stock stock = db.Stocks.Find(id);
            db.Stocks.Remove(stock);
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
