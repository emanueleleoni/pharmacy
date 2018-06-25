using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Model
{
    public enum MachineErrorLevel { warning, critical }

    public class MachineError
    {
        [Key]
        public Guid ErrorID { get; set; }

        public Guid MachineID { get; set; }

        public DateTime Date { get; set; }

        public string Message { get; set; }

        public MachineErrorLevel Level { get; set; }
    }
}