using Admin.Models;
using DAL.Context;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace Admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            ViewBag.LatestOrders        = db.Transactions.OrderByDescending(x => x.TransactionDate).Take(10).ToList();
            ViewBag.NewOrders           = db.Transactions.Where(a => a.Status == TransactionStatus.Accepted && a.Shipped == false && a.Delivered == false).Count();
            ViewBag.ShippedOrders       = db.Transactions.Where(a => a.Status == TransactionStatus.Accepted && a.Shipped == true &&  a.Delivered == false).Count();
            ViewBag.DeliveredOrders     = db.Transactions.Where(a => a.Status == TransactionStatus.Accepted && a.Shipped == true && a.Delivered == true).Count();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //Area Grap
        public JsonResult GetSalesPerDay()
        {
            var data = (from O in db.Transactions
                        select new { date = System.Data.Entity.DbFunctions.TruncateTime(O.TransactionDate), O.TotalAmount } into a
                        group a by a.date into b
                        select new
                        {
                            period = b.Key,
                            sales = b.Sum(x => x.TotalAmount)
                        });

            List<AreaCharts> aa = new List<AreaCharts>();
            foreach (var item in data.OrderBy(d => d.period))
            {
                string date = item.period.Value.ToString("yyyy-MM-dd");
                aa.Add(new AreaCharts() { period = date, sales = Convert.ToInt32(item.sales) });
            }
            return Json(aa, JsonRequestBehavior.AllowGet);
        }

        //Circle Graph
        public JsonResult GetTopProductSales()
        {
            var dataforchart = (from OD in db.TransactionDetails
                                join P in db.Products
                                on OD.ProductID equals P.ProductID
                                select new { P.Name, OD.Quantity } into row
                                group row by row.Name into g
                                select new
                                {
                                    label = g.Key,
                                    value = g.Sum(x => x.Quantity)
                                })
                    .OrderByDescending(x => x.value)
                    .Take(3);
            return Json(dataforchart, JsonRequestBehavior.AllowGet);
        }

        //Line Grap
        public JsonResult GetOrderPerDay()
        {
            var data = from O in db.Transactions
                       select new { Odate = System.Data.Entity.DbFunctions.TruncateTime(O.TransactionDate), O.TransactionID } into g
                       group g by g.Odate into col
                       select new
                       {
                           Order_Date = col.Key,
                           Count = col.Count(y => y.TransactionID != null)
                       };
            List<LineCharts> aa = new List<LineCharts>();
            foreach (var item in data)
            {
                string date = item.Order_Date.Value.ToString("yyyy-MM-dd");
                aa.Add(new LineCharts() { Date = date, Orders = item.Count });
            }
            return Json(aa, JsonRequestBehavior.AllowGet);
        }

        //Bar Grap
        public JsonResult GetSalesPerCountry()
        {
            var dataforBarchart = (from O in db.Transactions
                                   select new { O.Type, O.TotalAmount } into row
                                   group row by row.Type into g
                                   select new
                                   {
                                       Type = g.Key,
                                       Sales_Amount = g.Sum(x => x.TotalAmount)
                                   })
                              .OrderByDescending(x => x.Sales_Amount)
                              .Take(2);
            return Json(dataforBarchart, JsonRequestBehavior.AllowGet);
        }
    }
}