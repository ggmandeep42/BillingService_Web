using System;
namespace BillingService.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public string Price { get; set; }
        public string IsDrink { get; set; }
    }
}
