using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopping.Web.Models
{
    [Serializable]
    public class EAgreementException : Exception
    {
        public EAgreementException(string message)
            : base(message)
        {
        }
    }
}