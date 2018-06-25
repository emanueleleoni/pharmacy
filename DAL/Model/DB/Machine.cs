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
    }
}