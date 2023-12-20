using Microsoft.AspNetCore.Mvc;
using SVSTTest002Lib;
using SVSTTest02.Models;
using System.Diagnostics;

namespace SVSTTest02.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

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

        public async Task<string> AcceptTheDataAsync(string id, string timeStamp, string h2Value, string o2value)
        {
            string json = "ok";

            GAS_VALUESModel clientPackage = new GAS_VALUESModel(int.Parse(id), DateTime.Parse(timeStamp), double.Parse(h2Value), double.Parse(o2value));

            ServerResponceModel serverResponce = new ServerResponceModel(clientPackage.GAS_VAL_ID, DateTime.Now);
            json = await serverResponce.GetJsonFromModel();

            return json;
        }
    }
}