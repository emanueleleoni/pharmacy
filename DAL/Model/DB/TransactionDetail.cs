using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Model
{
    public class TransactionDetail
    {
        [ForeignKey("Transaction")]
        public Guid TransactionDetailID { get; set; }

        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalAmount { get; set; }

        public Guid ProductID { get; set; }
        public Product Product { get; set; }

        public virtual Transaction Transaction { get; set; }
    }
}