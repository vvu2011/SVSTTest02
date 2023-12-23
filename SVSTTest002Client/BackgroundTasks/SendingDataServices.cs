using SVSTTest002Lib;

namespace SVSTTest002Client.BackgroundTasks
{
    public class SendingDataServices : BackgroundService
    {
        public int PackageId { get; set; }

        // log
        string fileName { get; set; }
        string wwwrootPath { get; set; } 
        string filePath { get; set; }

        public SendingDataServices()
        {

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            fileName = "log.txt";
            wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            filePath = Path.Combine(wwwrootPath, fileName);

            while (!stoppingToken.IsCancellationRequested)
            {
                // генерация посылки
                PackageId++;
                Random random = new Random();
                GAS_VALUESModel clientPackage = new GAS_VALUESModel(DateTime.Now, Math.Round(random.NextDouble() * 10, 3), Math.Round(random.NextDouble() * 10, 3));

                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        var parameters = new Dictionary<string, string>
                        {
                            { "Id", clientPackage.GAS_VAL_ID.ToString() },
                            { "Timestamp", clientPackage.GAS_VAL_DATE.ToString() },
                            { "H2Value", clientPackage.H2_VAL.ToString() },
                            { "O2Value", clientPackage.O2_VAL.ToString() },
                        };

                        string url = "http://svsttest02:80/Home/AcceptTheData" + "?" + new FormUrlEncodedContent(parameters).ReadAsStringAsync().Result; ;

                        HttpResponseMessage response = await client.GetAsync(url);
                        response.EnsureSuccessStatusCode();

                        string responseBody = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(responseBody);

                        await LoggingToFileAsync(responseBody);
                    }
                    catch (HttpRequestException ex)
                    {
                        Console.WriteLine(ex.Message);
                        await LoggingToFileAsync(ex.Message);

                    }
                }

                await Task.Delay(TimeSpan.FromSeconds(0), stoppingToken);
            }
        }

        private async Task<string> LoggingToFileAsync(string row)
        {
            string status = "ok";

            // лог
            try
            {
                if (File.Exists(filePath))
                {
                    using (StreamWriter writer = File.AppendText(filePath))
                    {
                        writer.WriteLine(row);
                    }
                }
                else
                {
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        writer.WriteLine(row);
                    }
                }
            }
            catch (Exception ex2)
            {
                Console.WriteLine(ex2.Message);
            }

            await Task.Delay(0);
            return status;
        }
    }
}