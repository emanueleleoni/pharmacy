using DAL.Context;
using DAL.Model;
using Marcucci.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Marcucci.Controllers
{
    public class MyCartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MyCart
        public ActionResult Index()
        {
            var data = this.GetDefaultData();

            return View(data);
        }

        public ActionResult Remove(Guid id)
        {
            TempShpData.items.RemoveAll(x => x.ProductID == id);
            return RedirectToAction("Index");

        }
        [HttpPost]
        public ActionResult ProcedToCheckout(FormCollection formcoll)
        {
            var a = TempShpData.items.ToList();
            for (int i = 0; i < formcoll.Count / 2; i++)
            {
                var pID =  Guid.Parse(formcoll["shcartID-" + i + ""]);
                var ODetails = TempShpData.items.FirstOrDefault(x => x.ProductID == pID);

                int qty = Convert.ToInt32(formcoll["Qty-" + i + ""]);
                ODetails.Quantity = qty;
                ODetails.UnitPrice = ODetails.UnitPrice;
                ODetails.TotalAmount = qty * ODetails.UnitPrice;
                TempShpData.items.RemoveAll(x => x.ProductID == pID);

                if (TempShpData.items == null)
                    TempShpData.items = new List<TransactionDetail>();

                TempShpData.items.Add(ODetails);
            }

            return RedirectToAction("Index", "CheckOut");
        }
    }
}