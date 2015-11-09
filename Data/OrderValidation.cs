using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data
{
    [MetadataType(typeof(OrderMetaData))]
    public partial class Order
    {
        public class OrderMetaData
        {
            [Key]
              public int PKOrderId { get; set; }
            
           

        }
    }
}
