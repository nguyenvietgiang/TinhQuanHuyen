using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using TinhQuanHuyen.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TinhQuanHuyen.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        string baseURL = "http://dev.s-erp.com.vn:9038/v1/countries/VN/";
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseURL);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        HttpResponseMessage getData = await client.GetAsync("provinces?page=1&size=66");
                        if (getData.IsSuccessStatusCode)
                        {
                            string result = getData.Content.ReadAsStringAsync().Result;
                            var res = JsonConvert.DeserializeObject<ProvinceMasterResponseModel>(result);
                            ProvinceResponseModelPagination resPagi = new ProvinceResponseModelPagination();
                            if (res != null && res.Code == 200)
                            {
                                resPagi.CurrentPage = res.Data.CurrentPage;
                                resPagi.TotalPages = res.Data.TotalPages;
                                resPagi.PageSize = res.Data.PageSize;
                                resPagi.NumberOfRecords = res.Data.NumberOfRecords;
                                resPagi.TotalRecords = res.Data.TotalRecords;
                                resPagi.Content = res.Data.Content;
                            }
                            return View(resPagi);
                        }
                        else
                        {
                            Console.WriteLine("Error calling web api");
                        }
                    }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(int province)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await client.GetAsync("provinces/"+province+ "/districts");
                if (getData.IsSuccessStatusCode)
                {
                    string result = getData.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<DistrictsMasterResponseModel>(result);
                    DistrictsResponseModelPagination resPagi2 = new DistrictsResponseModelPagination();
                    if (res != null && res.Code == 200)
                    {
                        resPagi2.CurrentPage = res.Data.CurrentPage;
                        resPagi2.TotalPages = res.Data.TotalPages;
                        resPagi2.PageSize = res.Data.PageSize;
                        resPagi2.NumberOfRecords = res.Data.NumberOfRecords;
                        resPagi2.TotalRecords = res.Data.TotalRecords;
                        resPagi2.Content = res.Data.Content;
                    }
                    return View(resPagi2);
                }
                else
                {
                    Console.WriteLine("Error calling web api");
                }
            }
            return View();
        }

            public IActionResult Privacy()
        {
            return View();
        }
        public async Task<JsonResult> GetSubCategories(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await client.GetAsync("provinces/" + id + "/districts");
                if (getData.IsSuccessStatusCode)
                {
                    string result = getData.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<DistrictsMasterResponseModel>(result);
                    DistrictsResponseModelPagination resPagi2 = new DistrictsResponseModelPagination();
                    if (res != null && res.Code == 200)
                    {
                        resPagi2.CurrentPage = res.Data.CurrentPage;
                        resPagi2.TotalPages = res.Data.TotalPages;
                        resPagi2.PageSize = res.Data.PageSize;
                        resPagi2.NumberOfRecords = res.Data.NumberOfRecords;
                        resPagi2.TotalRecords = res.Data.TotalRecords;
                        resPagi2.Content = res.Data.Content;
                    }
                    return Json(resPagi2);
                }
                else
                {
                    Console.WriteLine("Error calling web api");
                }
                DistrictsResponseModelPagination resPagi3 = new DistrictsResponseModelPagination();
                return Json(resPagi3);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}