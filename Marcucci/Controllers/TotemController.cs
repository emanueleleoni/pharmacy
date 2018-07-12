using DAL.Context;
using DAL.Model;
using Microsoft.AspNet.Identity;
using Microsoft.Azure.Devices;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Marcucci.Controllers
{
    //[Authorize]
    public class TotemController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        static ServiceClient serviceClient;
        static string connectionString = "HostName=pagitamqtt.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=WtlGCPngdldCtoISN0Y4785ErP4sMvimUBOyutIRs2o=";

        // GET: Totem
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetRfid()
        {
            MessagesRepository _messageRepository = new MessagesRepository();
            var model = _messageRepository.GetAllMessages();

            return model.FirstOrDefault().Rfid;
        }

        public ActionResult Rfid()
        {
            MessagesRepository _messageRepository = new MessagesRepository();
            _messageRepository.SetToReaded();

            // Invoke the direct method on the device, passing the payload
            serviceClient = ServiceClient.CreateFromConnectionString(connectionString);
            var methodInvocation = new CloudToDeviceMethod("GetRfid") { ResponseTimeout = TimeSpan.FromSeconds(30) };

            // Invoke the direct method asynchronously and get the response from the simulated device.
            var response = serviceClient.InvokeDeviceMethodAsync("raspberry", methodInvocation);
            return View();
        }

        public ActionResult List()
        {
            MessagesRepository _messageRepository = new MessagesRepository();
            _messageRepository.SetToReaded();

            var prods = db.Stocks.Include("Product").Where(x => x.Quantity > 0).ToList();

            return View(prods);
        }

        public ActionResult Purchase(string id)
        {
            var stock = db.Stocks.Include("Product").FirstOrDefault(x => x.StockID.ToString() == id);
            if (stock == null)
                throw new Exception("Errore!! Lo Stock selezionato non esiste");

            var username = User.Identity.GetUserName();

            var machineReservation = new MachineReservation
            {
                ConsumerID = username,
                IsDeleted = false,
                IsLocked = false,
                MachineID = stock.MachineID,
                MachineReservationID = Guid.NewGuid(),
                RequestDate = DateTime.Now,
                Status = MachineReservationStatus.Reserved
            };

            var transactionDetail = new TransactionDetail
            {
                ProductID = stock.ProductID,
                Quantity = 1,
                TotalAmount = stock.Product.Price,
                TransactionDetailID = Guid.NewGuid(),
                UnitPrice = stock.Product.Price
            };

            var user = db.Users.FirstOrDefault(x => x.UserName == username);

            var shippingDetail = new ShippingDetail { 
                Address = "Via Cantalupo, 4",
                City = "Cesenatico",
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Mobile = user.PhoneNumber,
                PostalCode = "47042",
                Province = "FC",
                ShippingID = Guid.NewGuid()             
            };

            var transaction = new Transaction
            {
                CashChannel = CashChannelType.Unknown,
                ConsumerID = username,
                MachineID = stock.MachineID,
                MachineReservation = machineReservation,
                Status = TransactionStatus.InProgress,
                TotalAmount = stock.Product.Price,
                TransactionDate = DateTime.Now,
                Type = TransactionType.Totem,
                TransactionID = Guid.NewGuid(),
                TransactionDetail = transactionDetail,
                ShippingDetail = shippingDetail
            };

            db.Transactions.Add(transaction);

            db.SaveChanges();

            MessagesRepository _messageRepository = new MessagesRepository();
            _messageRepository.SetToReaded();

            // Invoke the direct method on the device, passing the payload
            serviceClient = ServiceClient.CreateFromConnectionString(connectionString);
            var methodInvocation = new CloudToDeviceMethod("Purchase") { ResponseTimeout = TimeSpan.FromSeconds(30) };
            methodInvocation.SetPayloadJson("{ \"transactionid\": \""+ transaction.TransactionID + "\", \"total\": \"" + stock.Product.Price + "\", \"selection\": \"" + stock.MachineKeyBoardNumber + "\" }");

            // Invoke the direct method asynchronously and get the response from the simulated device.
            var response = serviceClient.InvokeDeviceMethodAsync("raspberry", methodInvocation);

            return View(stock);
        }

        public string GetTransactionResult()
        {
            MessagesRepository _messageRepository = new MessagesRepository();
            var model = _messageRepository.GetAllMessages();

            return model.FirstOrDefault().Payment; //model.FirstOrDefault().Payment;
        }

        public ActionResult Thankyou(string id)
        {
            var transaction = db.Transactions.FirstOrDefault(x => x.TransactionID.ToString() == id);
            transaction.Status = TransactionStatus.Accepted;

            db.SaveChanges();

            return View();
        }

        public ActionResult Error(string id){
            return View();
        }
    }
}