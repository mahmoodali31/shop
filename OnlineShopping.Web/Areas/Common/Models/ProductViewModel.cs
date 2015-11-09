using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopping.Web.Areas.Common.Models
{
    public class ProductViewModel
    {
        public int PKProductId { get; set; }
      
        public string ProductName { get; set; }
       
        public int Quantity { get; set; }
        public Nullable<decimal> Price { get; set; }
      
        
    }
}