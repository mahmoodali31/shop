using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{

    public class UniqueEmailAttribute : ValidationAttribute
    {
        //protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        //{
        //    ShoppingCartEntities db = new ShoppingCartEntities();
        //    string userEmailValue = value.ToString();
        //    int count = db.Users.Where(x => x.EmailId == userEmailValue).ToList().Count();
        //    if (count != 0)
        //        return new ValidationResult("User Already Exist With This Email ID");
        //    return ValidationResult.Success;
        //}
    }
    [MetadataType(typeof(UserMetaData))]
    public partial class User
    {
        public string Date { get; set; }
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        public class UserMetaData
        {
            [Key]
            public int PKUserId { get; set; }
            public string UserName { get; set; }
            [Required]
            public string FirstName { get; set; }
            [Required]
            public string LastName { get; set; }
            [Required]
            //[UniqueEmail]
            public string EmailId { get; set; }
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
           


        }
    }
}
