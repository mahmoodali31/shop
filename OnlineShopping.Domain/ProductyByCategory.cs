using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Domain
{
   public  class ProductyByCategory
    {
       [Key]
        public int PKProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string LargeImagePath { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> Price { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
        public bool IsActive { get; set; }
        public string CategoryName { get; set; }
        public Nullable<int> FKProductId { get; set; }
    }
}
