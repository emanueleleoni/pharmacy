using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Model
{
    public enum Author { machine, server }

    public class MachineMessage
    {
        [Key]
        public Guid MachineMessageID { get; set; }

        public DateTime Date { get; set; }

        public Author AuthorType { get; set; }

        public string Message { get; set; }

        public string MessageType { get; set; }
        public Guid MachineID { get; set; }
        public Machine Machine { get; set; }
    }
}