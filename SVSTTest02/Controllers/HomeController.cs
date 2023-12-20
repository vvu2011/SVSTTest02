using Microsoft.AspNetCore.Mvc;
using SVSTTest002Lib;
using SVSTTest02.Data;
using SVSTTest02.Models;
using System.Diagnostics;

namespace SVSTTest02.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDataContextModel _context;

        public HomeController(ILogger<HomeController> logger, AppDataContextModel context)
        {
            _logger = logger;
            _context = context;
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

            GAS_VALUESModel clientPackage = new GAS_VALUESModel(DateTime.Parse(timeStamp).ToUniversalTime(), double.Parse(h2Value), double.Parse(o2value));

            await _context.GAS_VALUES.AddAsync(clientPackage);
            await _context.SaveChangesAsync();

            ServerResponceModel serverResponce = new ServerResponceModel(clientPackage.GAS_VAL_ID, DateTime.Now);
            json = await serverResponce.GetJsonFromModel();

            return json;
        }
    }
}