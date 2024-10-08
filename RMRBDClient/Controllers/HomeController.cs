using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RMRBDClient.Models;
using System.Diagnostics;

namespace RMRBDClient.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IConfiguration configuration) : base(configuration) { }

        
        public async Task<IActionResult> HomePage()
        {
            if(HttpContext.Session.GetString("CustomerId") != null)
            {
                ViewData["CustomerId"] = HttpContext.Session.GetString("CustomerId");
                ViewData["UserName"] = HttpContext.Session.GetString("UserName");
                ViewData["Coin"] = HttpContext.Session.GetString("Coin");
                ViewData["Avatar"] = HttpContext.Session.GetString("Avatar");
            }
            HttpResponseMessage response = await _httpClient.GetAsync($"{RecipeUrl}?$filter=Status eq 1");
            response.EnsureSuccessStatusCode();
            List<Recipe> recipes = JsonConvert.DeserializeObject<List<Recipe>>(response.Content.ReadAsStringAsync().Result);
            return View(recipes);
        }

        public IActionResult LandingPage()
        {

            return View();
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
