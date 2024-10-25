using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using RMRBDClient.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace RMRBDClient.Controllers
{
    public class CustomerController : BaseController
    {

        public CustomerController(IConfiguration configuration) : base(configuration) { }
        public async Task<IActionResult> PersonalProfile()
        {
            if (HttpContext.Session.GetInt32("CustomerId") != null)
            {
                HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync($"{CustomerUrl}?$filter=CustomerId eq {HttpContext.Session.GetInt32("CustomerId")}");
                httpResponseMessage.EnsureSuccessStatusCode();
                var customer = JsonConvert.DeserializeObject<List<Customer>>(httpResponseMessage.Content.ReadAsStringAsync().Result).FirstOrDefault();

                ViewData["CustomerId"] = customer.CustomerId;
                ViewData["UserName"] = customer.UserName;
                ViewData["Coin"] = customer.Coin;
                ViewData["Avatar"] = customer.Avatar;
                ViewData["SellerStatus"] = customer.SellerStatus;

                return View(customer);
            }
            return RedirectToAction("Logout", "LoginPages");
        }

        public async Task<IActionResult> SellerRegister()
        {
            //if (HttpContext.Session.GetInt32("CustomerId") != null)
            //{
            //    HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync($"{CustomerUrl}?$filter=CustomerId eq {HttpContext.Session.GetInt32("CustomerId")}");
            //    httpResponseMessage.EnsureSuccessStatusCode();
            //    var customer = JsonConvert.DeserializeObject<List<Customer>>(httpResponseMessage.Content.ReadAsStringAsync().Result).FirstOrDefault();
            //
            //    ViewData["CustomerId"] = customer.CustomerId;
            //    ViewData["UserName"] = customer.UserName;
            //    ViewData["Coin"] = customer.Coin;
            //    ViewData["Avatar"] = customer.Avatar;
            //    ViewData["SellerStatus"] = customer.SellerStatus;
            //}
            //else
            //{
            //    return RedirectToAction("Logout", "LoginPages");
            //    
            //}
            ViewData["provinces"] = await GetProvinces();
            return View();
            
            
        }

        public async Task<IEnumerable<Province>> GetProvinces()
        {

            var request = new HttpRequestMessage(HttpMethod.Get, $"{GHNUrl}/master-data/province");
            request.Headers.Add("Token", GHNToken);

            HttpResponseMessage httpResponseMessage = await _httpClient.SendAsync(request);

            httpResponseMessage.EnsureSuccessStatusCode();

            var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<JObject>(responseContent);

            var dataArray = responseObject["data"] as JArray;

            var provinces = dataArray.ToObject<List<Province>>();

            return provinces;
        }

        public async Task<IActionResult> AjaxTest()
        {
            return View();
        }
        
    }
}
