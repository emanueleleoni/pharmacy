using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DAL.Model
{
    public class Stock
    {
        [Key]
        public Guid StockID { get; set; }

        public int MachineKeyBoardNumber { get; set; }

        public int Quantity { get; set; }

        public Guid MachineID { get; set; }
        public Machine Machine { get; set; }

        public Guid ProductID { get; set; }
        public Product Product { get; set; }
    }
}