namespace SVSTTest002Client.BackgroundTasks
{
    public class SendingDataServices : BackgroundService
    {
        public SendingDataServices()
        {
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        HttpResponseMessage response = await client.GetAsync("http://svsttest02:80/Home/AcceptTheData/");
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