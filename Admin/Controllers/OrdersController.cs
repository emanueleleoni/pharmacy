using DAL.Context;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Orders
        public ActionResult Index()
        {
            return View(db.Transactions.OrderByDescending(x => x.TransactionDate).ToList());
        }

        public ActionResult Details(string id)
        {
            var ord = db.Transactions.Include("ShippingDetail").FirstOrDefault(q => q.TransactionID.ToString() == id);
            var Ord_details = db.TransactionDetails.Include("Product").Where(q => q.Transaction.TransactionID.ToString() == id);
            var tuple = new Tuple<Transaction, IEnumerable<TransactionDetail>>(ord, Ord_details);

            double SumAmount = Convert.ToDouble(Ord_details.Sum(x => x.TotalAmount));
            ViewBag.TotalItems = Ord_details.Sum(x => x.Quantity);
            ViewBag.Discount = 0;
            ViewBag.TAmount = SumAmount - 0;
            ViewBag.Amount = SumAmount;
            return View(tuple);
        }
    }
}