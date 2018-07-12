using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class Message
    {
        public int Id { get; set; }
        public string Rfid { get; set; }
        public string Cf { get; set; }
        public string Payment { get; set; }
        public bool Readed { get; set; }
        public DateTime Date { get; set; }
    }
}
