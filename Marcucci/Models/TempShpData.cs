using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Marcucci.Models
{
    public static class TempShpData
    {
        public static int UserID { get; set; }
        public static List<TransactionDetail> items { get; set; }
    }
}