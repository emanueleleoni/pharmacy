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
    public static class LoadDataController
    {
        static ApplicationDbContext db = new ApplicationDbContext();

        public static List<TransactionDetail> GetDefaultData(this ControllerBase controller)
        {
            if (TempShpData.items == null)
                TempShpData.items = new List<TransactionDetail>();

            var data = TempShpData.items.ToList();

            controller.ViewBag.cartBox  = data.Count == 0 ? null : data;
            controller.ViewBag.NoOfItem = data.Count();
            int? SubTotal = Convert.ToInt32(data.Sum(x => x.TotalAmount));
            controller.ViewBag.Total = SubTotal;

            int Discount = 0;
            controller.ViewBag.SubTotal = SubTotal;
            controller.ViewBag.Discount = Discount;
            controller.ViewBag.TotalAmount = SubTotal - Discount;

            return data;
        }
    }
}