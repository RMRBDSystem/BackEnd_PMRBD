using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace RMRBDClient.Controllers
{
    public class CustomerController : BaseController
    {
        public CustomerController(IConfiguration configuration) : base(configuration) { }
        
    }
}
