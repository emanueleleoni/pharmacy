using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;

namespace DAL.Model
{
    public class Machine
    {
        [Key]
        public Guid MachineID { get; set; }

        /// <summary> the Machine Code, which is corresponds to the sticker label attached to the
        /// physical machine. NFC & QRCode should return this value </summary>
        public string Code { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public DateTime? LastUpdate { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<MachineError> MachineErrors { get; set; }
        public ICollection<MachineMessage> MachineMessages { get; set; }
        public ICollection<MachineReservation> MachineReservations { get; set; }
        public ICollection<Stock> Stocks { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}