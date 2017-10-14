using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FridgeRestServer.Models
{
    public class Product
    {
        public int? Id { get; set; }
        public string PersonLogin { get; set; }
        public string Name { get; set; }
        public float? Price { get; set; }
        public int? Amount { get; set; }
    }
}