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

        public IActionResult Index()
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
        public IActionResult Test()
        {
            return View();
        }
    }
}