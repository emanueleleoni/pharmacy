using DAL.Context;
using DAL.Model;
using Marcucci.Models;
using Microsoft.Azure.Devices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace Marcucci.Controllers
{
    [Authorize]
    public class CheckOutController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        static ServiceClient serviceClient;
        static string connectionString = "HostName=pagitamqtt.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=WtlGCPngdldCtoISN0Y4785ErP4sMvimUBOyutIRs2o=";

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

            // SEND MESSAGE TO TOTEM
            //serviceClient = ServiceClient.CreateFromConnectionString(connectionString);
            //var commandMessage = new Microsoft.Azure.Devices.Message(Encoding.ASCII.GetBytes("Cloud to device message."));
            //serviceClient.SendAsync("raspberry", commandMessage);

            // Invoke the direct method on the device, passing the payload
            serviceClient = ServiceClient.CreateFromConnectionString(connectionString);
            var methodInvocation = new CloudToDeviceMethod("stop") { ResponseTimeout = TimeSpan.FromSeconds(30) };
            methodInvocation.SetPayloadJson("10");

            // Invoke the direct method asynchronously and get the response from the simulated device.
            var response = serviceClient.InvokeDeviceMethodAsync("raspberry", methodInvocation);

            return RedirectToAction("Index", "ThankYou");
        }
    }
}