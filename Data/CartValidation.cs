using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    [MetadataType(typeof(CartMetaData))]
    public partial class ShoppingCart
    {

        public class CartMetaData
        {
            [Key]
            public int CartId { get; set; }
          


        }
    }
}
