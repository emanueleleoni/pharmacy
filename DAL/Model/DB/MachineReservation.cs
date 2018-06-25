using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Model
{
    public enum MachineReservationStatus { Init, Reserved, Dropped, Error, UnDropped, TimeOut }

    public class MachineReservation
    {
        [ForeignKey("Transaction")]
        public Guid MachineReservationID { get; set; }

        public DateTime RequestDate { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public bool IsLocked { get; set; }

        public string ConsumerID { get; set; }

        public MachineReservationStatus Status { get; set; }

        public bool IsDeleted { get; set; }

        public Guid MachineID { get; set; }
        public Machine Machine { get; set; }

        public virtual Transaction Transaction { get; set; }
    }
}