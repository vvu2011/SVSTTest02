using SVSTTest002Lib;

namespace SVSTTest002Client.BackgroundTasks
{
    public class SendingDataServices : BackgroundService
    {
        public int PackageId { get; set; }

        public SendingDataServices()
        {
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // генерация посылки
                PackageId++;
                Random random = new Random();
                GAS_VALUESModel clientPackage = new GAS_VALUESModel(PackageId, DateTime.Now, Math.Round(random.NextDouble() * 10, 3), Math.Round(random.NextDouble() * 10, 3));

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
                    }
                    catch (HttpRequestException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            }
        }
    }
}