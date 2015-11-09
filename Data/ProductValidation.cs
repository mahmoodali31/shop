using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{

     [MetadataType(typeof(ProductMetaData))]
    public partial class Product
    {
        public class ProductMetaData
        {
            [Key]
            public int PKProductId { get; set; }
             [DisplayFormat(DataFormatString = "{0:c}")]
            public Nullable<decimal> Price { get; set; }
        }
    
    }
}
