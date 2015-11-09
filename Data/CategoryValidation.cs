using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
      [MetadataType(typeof(CategoryMetaData))]
    public partial class Category
    {
          public class CategoryMetaData
        {
              [Key]
              public int PKCategoryId { get; set; }
               [Required]
              public string CategoryName { get; set; }
               [Required]
              public string Description { get; set; }
             
        }
    }

    
}
