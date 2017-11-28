using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FridgeRestServer.Models
{
    public class Amount
    {
        public string Guid { get; set; }
        public int? ProductId { get; set; }
        public int? Value { get; set; }

        public static int? getAmount(List<Amount> amounts)
        {
            int? value = 0;
            foreach (var amount in amounts)
            {
                value += amount.Value;
            }
            return value;
        }
    }
}