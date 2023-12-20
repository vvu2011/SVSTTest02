using Microsoft.AspNetCore.Mvc;
using SVSTTest002Lib;
using SVSTTest02.Data;
using SVSTTest02.Models;
using System.Diagnostics;
using System.Text.Json;

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

        /// <summary>
        /// Запись физ. параметров в бд
        /// </summary>
        /// <param name="id"></param>
        /// <param name="timeStamp"></param>
        /// <param name="h2Value"></param>
        /// <param name="o2value"></param>
        /// <returns></returns>
        public async Task<string> AcceptTheDataAsync(string id, string timeStamp, string h2Value, string o2value)
        {
            string json = "ok";

            // Преобразование DateTime с Kind=Unspecified в DateTime с Kind=Utc
            GAS_VALUESModel clientPackage = new GAS_VALUESModel(DateTime.Parse(timeStamp).ToUniversalTime(), double.Parse(h2Value), double.Parse(o2value));

            await _context.GAS_VALUES.AddAsync(clientPackage);
            await _context.SaveChangesAsync();

            ServerResponceModel serverResponce = new ServerResponceModel(clientPackage.GAS_VAL_ID, DateTime.Now);
            json = await serverResponce.GetJsonFromModel();

            return json;
        }

        /// <summary>
        /// Запрос данных для диаграммы
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetDataForChartAsync()
        {
            string json = "ok";

            // запрос данных из таблицы
            DateTime begin = DateTime.UtcNow.AddMinutes(-5);
            List<GAS_VALUESModel> valsUtcTime = _context.GAS_VALUES
                .Where(s => s.GAS_VAL_DATE > begin)
                .ToList();

            TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time");

            // коррекция с учетов временной зоны
            List<GAS_VALUESModel> valsLocalTime = new List<GAS_VALUESModel>();
            foreach (var item in valsUtcTime)
            {
                valsLocalTime.Add(new GAS_VALUESModel(item.GAS_VAL_DATE + timeZone.BaseUtcOffset, item.H2_VAL, item.O2_VAL));
            }

            json = JsonSerializer.Serialize(valsLocalTime);

            return json;
        }
    }
}