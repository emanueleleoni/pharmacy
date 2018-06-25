using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Model
{
    public enum MachineReservationStatus { Init, Reserved, Dropped, Error, UnDropped, TimeOut }

    public class MachineReservation
    {
        [Key]
        public Guid MachineReservationID { get; set; }

        public DateTime RequestDate { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public bool IsLocked { get; set; }

        public string ConsumerID { get; set; }

        public Guid MachineID { get; set; }

        public MachineReservationStatus Status { get; set; }

        public Guid TransactionID { get; set; }

        public bool IsDeleted { get; set; }
    }
}