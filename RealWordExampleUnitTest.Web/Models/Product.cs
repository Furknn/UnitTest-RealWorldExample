using System;
using System.Collections.Generic;

namespace RealWordExampleUnitTest.Web.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductColor { get; set; }
        public int? ProductStock { get; set; }
        public decimal? ProductPrice { get; set; }
    }
}
