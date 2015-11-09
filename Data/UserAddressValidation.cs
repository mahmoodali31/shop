using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    [MetadataType(typeof(UserAddressMetaData))]
    public partial class UserAddress
    {
        public class UserAddressMetaData
        {
            [Key]
            public int PKShippingAddressId { get; set; }



        }
    }
}
