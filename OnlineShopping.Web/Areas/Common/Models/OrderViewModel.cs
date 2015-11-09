using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data;

namespace OnlineShopping.Web.Areas.Common.Models
{
    public class OrderViewModel
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int Zipcode { get; set; }
        public string Role { get; set; }



        public List<ShoppingCart> Carts { get; set; }


     
       
      
        public Nullable<decimal> TotalPrice { get; set; }
      

    }
}