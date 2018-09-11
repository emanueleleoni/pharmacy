using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class Product
    {
        [Key]
        public Guid ProductID { get; set; }

        public string MarcucciCode { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public bool Available { get; set; }

        public bool IsDeleted { get; set; }

        public double Price { get; set; }

        public string ImageUrl { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        public string Note { get; set; }

        public bool ForAdult { get; set; }

        // Un prodotto appartiene ad una categoria
        public Guid CategoryID { get; set; }
        public Category Category { get; set; }

        public ICollection<Stock> Stocks { get; set; }
        public ICollection<TransactionDetail> TransactionDetails { get; set; }
    }
}
