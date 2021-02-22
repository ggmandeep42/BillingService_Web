using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BillingService.Web.Models;
using BillingService.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace BillingService.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClient;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _httpClient = clientFactory;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(OrderViewModel model)
        {
            
            var menuItems = new Dictionary<string, decimal>();
            decimal total = 0;

            var client = _httpClient.CreateClient("Billing_API");

            using (var response = await client.GetAsync("billingservice"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                menuItems = JsonConvert.DeserializeObject<Dictionary<string, decimal>>(apiResponse);
            }
                            
            //Assuming User enters only positive whole values
            total = (menuItems["Cola"] * model.ColaQuantity) + (menuItems["Coffee"] * model.CoffeeQuantity) +
                    (menuItems["CheeseSw"] * model.CheeseSwQuantity) + (menuItems["SteakSw"] * model.SteakSwQuantity);

            if (model.CheeseSwQuantity > 0 || model.SteakSwQuantity > 0)
            {
                total += (total * (10 / 100));
            }

            total = Math.Round(total, 2);

            model.Total = total;

            return View(model);
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
