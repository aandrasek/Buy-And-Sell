using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuyAndSell.VModels
{
    public class ListModel
    {
        public int ID { get; set; }
        [Display(Name = "Headline")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Area { get; set; }
        [Display(Name = "Phone number")]
        public string PhoneNum { get; set; }
        [Display(Name = "Price(in kuna)")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateofRel { get; set; }
        public int[] SelectedID { get; set; } = new int[1];
        [Display(Name = "Category")]
        public IEnumerable<SelectListItem> Items { get; set; }
        [Display(Name = "Main image")]
        public string MainImage { get; set; }
        [NotMapped]
        [Display(Name = "Images")]
        public virtual ICollection<HttpPostedFileBase> File { get; set; }
    }
}