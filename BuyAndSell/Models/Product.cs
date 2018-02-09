using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BuyAndSell.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Area { get; set; }
        public string PhoneNum { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateofRel { get; set; } 
        public int CategoryID { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public string MainImage { get; set; }
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [NotMapped]
        public virtual ICollection<HttpPostedFileBase> File { get; set; }

        public Product()
        {
            Categories = new List<Category>();
        }
    }
}