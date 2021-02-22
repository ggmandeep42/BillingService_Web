using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BillingService.Web.Models
{
    public class OrderViewModel
    { 
        public int ColaQuantity { get; set; }
        public int CoffeeQuantity { get; set; }
        public int CheeseSwQuantity { get; set; }
        public int SteakSwQuantity { get; set; }
        public decimal Total { get; set; }
    }
}
