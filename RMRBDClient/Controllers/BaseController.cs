using Microsoft.AspNetCore.Mvc;
using RMRBD_Client.Service;
using System.Net.Http.Headers;

namespace RMRBDClient.Controllers
{
    public class BaseController : Controller
    {
        protected readonly HttpClient _httpClient;
        protected readonly string BookUrl;
        protected readonly string BookCategoryUrl;
        protected readonly string BookOrderUrl;
        protected readonly string BookOrderDetailUrl;
        protected readonly string BookRateUrl;
        protected readonly string BookShelfUrl;
        protected readonly string CommentUrl;
        protected readonly string CustomerUrl;
        protected readonly string EbookUrl;
        protected readonly string EmployeeUrl;
        protected readonly string EmployeeTypeUrl;
        protected readonly string ImageUrl;
        protected readonly string NotificationUrl;
        protected readonly string PersonalRecipeUrl;
        protected readonly string RecipeUrl;
        protected readonly string RecipeRateUrl;
        protected readonly string ServiceFeedBackUrl;
        protected readonly string TagUrl;
        protected readonly string TransactionUrl;

        public BaseController(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);

            var apiSettings = new ApiSettings();
            configuration.GetSection("ApiUrls").Bind(apiSettings);

            BookUrl = apiSettings.BookUrl;
            BookCategoryUrl = apiSettings.BookCategoryUrl;
            BookOrderUrl = apiSettings.BookOrderUrl;
            BookOrderDetailUrl = apiSettings.BookOrderDetailUrl;
            BookRateUrl = apiSettings.BookRateUrl;
            BookShelfUrl = apiSettings.BookShelfUrl;
            CommentUrl = apiSettings.CommentUrl;
            CustomerUrl = apiSettings.CustomerUrl;
            EbookUrl = apiSettings.EbookUrl;
            EmployeeUrl = apiSettings.EmployeeUrl;
            EmployeeTypeUrl = apiSettings.EmployeeTypeUrl;
            ImageUrl = apiSettings.ImageUrl;
            NotificationUrl = apiSettings.NotificationUrl;
            PersonalRecipeUrl = apiSettings.PersonalRecipeUrl;
            RecipeUrl = apiSettings.RecipeUrl;
            RecipeRateUrl = apiSettings.RecipeRateUrl;
            ServiceFeedBackUrl = apiSettings.ServiceFeedBackUrl;
            TagUrl = apiSettings.TagUrl;
            TransactionUrl = apiSettings.TransactionUrl;
        }
    }
}