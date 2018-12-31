using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class OrderForm
    {
        public string ImageType { get; set; }
        public string URL { get; set; }
        public DateTime ExpireTime { get; set; }
        public Guid formGuid { get; set; }
    }
}
 