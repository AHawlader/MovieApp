using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MovieApp.Models
{
    public class Store
    {
        public int StoreId { get; set; }
        [DisplayName("Store Name")]
        public string StoreName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        [DisplayName("Zip Code")]
        public string ZipCode { get; set; }
        public string Country { get; set; }

    }
}