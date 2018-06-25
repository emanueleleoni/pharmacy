using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Model
{
    public class Category
    {
        [Key]
        public Guid CategoryID { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        // Una category può avere più prodotti
        public ICollection<Product> Products { get; set; }
    }
}
