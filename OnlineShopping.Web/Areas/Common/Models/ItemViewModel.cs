using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data;

namespace OnlineShopping.Web.Areas.Common.Models
{
    public class ItemViewModel
    {
        public List<ShoppingCart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}