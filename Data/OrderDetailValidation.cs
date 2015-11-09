using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{


    [MetadataType(typeof(OrderDetailMetaData))]
    public partial class OrderDetail
    {
        public class OrderDetailMetaData
        {
            [Key]
            public int PKOrderDetailsId { get; set; }
          

        }
    }
}
