using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuyAndSell.Models
{
    public class Category
    {
        public int ID { get; set; }
        public int? ParentID { get; set; }
        public virtual Category Parent { get; set; }
        public string Name { get; set; }
        public bool? Child { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}