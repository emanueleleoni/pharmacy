using DAL.Context;
using DAL.Model;
using Marcucci.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Marcucci.Controllers
{
    [Authorize]
    public class CheckOutController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CheckOut
        public ActionResult Index()
        {
            var items = new List<SelectListItem>();
            items.Add(new SelectListItem { Selected = true, Text = "Paypal", Value = CashChannelType.CreditCard.ToString() });

            ViewBag.PayMethod = new SelectList(items, "Value", "Text");

            var data = this.GetDefaultData();

            return View(data);
        }

        //PLACE ORDER--LAST STEP
        public ActionResult PlaceOrder(FormCollection getCheckoutDetails)
        {
            var shpID = Guid.NewGuid();
            var payID = Guid.NewGuid();
            var orderID = Guid.NewGuid();

            var shpDetails = new ShippingDetail();
            shpDetails.ShippingID   = shpID;
            shpDetails.FirstName    = getCheckoutDetails["FirstName"];
            shpDetails.LastName     = getCheckoutDetails["LastName"];
            shpDetails.Email        = getCheckoutDetails["Email"];
            shpDetails.Mobile       = getCheckoutDetails["Mobile"];
            shpDetails.Address      = getCheckoutDetails["Address"];
            shpDetails.Province     = getCheckoutDetails["Province"];
            shpDetails.City         = getCheckoutDetails["City"];
            shpDetails.PostalCode   = getCheckoutDetails["PostCode"];
            db.ShippingDetails.Add(shpDetails);
            db.SaveChanges();

            var o = new Transaction();
            o.TransactionID = orderID;
            o.ConsumerID = TempShpData.UserID.ToString();
            o.CashChannel = (CashChannelType)Convert.ToInt32(getCheckoutDetails["PaymentMethod"]);
            o.ShippingID = shpID;
            o.TotalAmount = Convert.ToInt32(getCheckoutDetails["totalAmount"]);
            o.TransactionDate = DateTime.Now;
            db.Transactions.Add(o);
            db.SaveChanges();

            foreach (var OD in TempShpData.items)
            {
                OD.TransactionDetailID = Guid.NewGuid();
                OD.Transaction = db.Transactions.Find(orderID);
                OD.Product = db.Products.Find(OD.ProductID);
                db.TransactionDetails.Add(OD);
                db.SaveChanges();
            }

            return RedirectToAction("Index", "ThankYou");
        }
    }
}