using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using BaitapDemo.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;

namespace BaitapDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        string baseURL = "http://dev.s-erp.com.vn:9038/v1/";
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index(TimezoneQueryModel model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage getData = await client.GetAsync("timezones?page=" + model.Page + "&size=" + model.PageSize + "&sort=" + model.Sort + "&filter={\"FullTextSearch\":\"" + model.FullTextSearch + "\"}");
                if (getData.IsSuccessStatusCode)
                {
                    string result = getData.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<TimezoneMasterResponseModel>(result);
                    TimezoneResponsePaginationModel resPagi = new TimezoneResponsePaginationModel();
                    if (res != null && res.Code == 200)
                    {
                        resPagi.CurrentPage = res.Data.CurrentPage;
                        resPagi.TotalPages = res.Data.TotalPages;
                        resPagi.PageSize = res.Data.PageSize;
                        resPagi.NumberOfRecords = res.Data.NumberOfRecords;
                        resPagi.TotalRecords = res.Data.TotalRecords;
                        resPagi.Content = res.Data.Content;
                    }
                    ViewBag.TotolPage = resPagi.TotalPages;
                    return View(resPagi);
                }
                else
                {
                    Console.WriteLine("Error calling web api");
                }
                return View();
            }
        }
        public IActionResult TinhThanh()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> Timezone(TimezoneQueryModel model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage getData = await client.GetAsync("timezones?page=" + model.Page + "&size=" + model.PageSize + "&sort=" + model.Sort + "&filter={\"FullTextSearch\":\"" + model.FullTextSearch + "\"}");
                if (getData.IsSuccessStatusCode)
                {
                    string result = getData.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<TimezoneMasterResponseModel>(result);
                    TimezoneResponsePaginationModel resPagi = new TimezoneResponsePaginationModel();
                    if (res != null && res.Code == 200)
                    {
                        resPagi.CurrentPage = res.Data.CurrentPage;
                        resPagi.TotalPages = res.Data.TotalPages;
                        resPagi.PageSize = res.Data.PageSize;
                        resPagi.NumberOfRecords = res.Data.NumberOfRecords;
                        resPagi.TotalRecords = res.Data.TotalRecords;
                        resPagi.Content = res.Data.Content;
                    }
                    ViewBag.TotolPage = resPagi.TotalPages;
                    return View(resPagi);
                }
                else
                {
                    Console.WriteLine("Error calling web api");
                }

                return View();
            }
        }
    }
}