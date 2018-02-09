using BuyAndSell.VModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BuyAndSell.Models
{
    public class BSContext:DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Images> Imagess { get; set; }
        public DbSet<ListModel> ListModels { get; set; }

    }
}