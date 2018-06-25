using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Model
{
    public enum TransactionType { Online, Totem }

    public enum TransactionStatus { Rejected, Accepted, Failed, InProgress }

    public enum CashChannelType { Unknown, BankNote, Coin, CreditCard }

    public class Transaction
    {
        [Key]
        public Guid TransactionID { get; set; }

        public DateTime TransactionDate { get; set; }

        public Guid ProductID { get; set; }

        public Guid ShippingID { get; set; }

        public Guid? MachineReservationID { get; set; }

        public Guid? MachineID { get; set; }

        public double TotalAmount { get; set; }

        public double Taxes { get; set; }

        public string ConsumerID { get; set; }

        public TransactionType Type { get; set; }

        public CashChannelType CashChannel { get; set; }

        public TransactionStatus Status { get; set; }

        public string ErrorID { get; set; }

        public bool Shipped { get; set; }

        public DateTime ShippingDate { get; set; }

        public bool Delivered { get; set; }

        public DateTime DeliveryDate { get; set; }

        public string Notes { get; set; }

        public TransactionDetail TransactionDetail { get; set; }
    }
}