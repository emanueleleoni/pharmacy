using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Model
{
    public class TransactionDetail
    {
        [Key]
        public Guid TransactionDetailID { get; set; }

        public Guid TransactionID { get; set; }
        public Guid ProductID { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalAmount { get; set; }
    }
}