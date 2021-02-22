using System;
using System.Collections.Generic;

namespace BillingService.Models
{
    public class Order
    {
        public List<KeyValuePair<string, int>> Items { get; set; }
    }
}
