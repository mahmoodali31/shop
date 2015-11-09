using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopping.Web.Areas.Common.Models
{
    public class CartViewModel
    {
      

        public IEnumerable<ProductCollection> ProductList { get; set; }
    }

    public class ProductCollection
    {
        public int PKProductId { get; set; }
         public string ProductName { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string LargeImagePath { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> Price { get; set; }
    }
}