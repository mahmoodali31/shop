using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data;

namespace OnlineShopping.Web.Models
{
    public class CategoryViewModel
    {
        public IEnumerable<int> Id { get; set; }
        public IEnumerable<Category> Name { get; set; }
    }
}