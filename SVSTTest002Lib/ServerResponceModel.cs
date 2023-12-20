using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SVSTTest002Lib
{
    public class ServerResponceModel
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }

        public ServerResponceModel()
        {
        }

        public ServerResponceModel(int id, DateTime timeStamp)
        {
            Id = id;
            TimeStamp = timeStamp;
        }

        public async Task<string> GetJsonFromModel()
        {
            string json = "ok";

            json = JsonSerializer.Serialize(this);
            await Task.Delay(0);

            return json;
        }
    }
}