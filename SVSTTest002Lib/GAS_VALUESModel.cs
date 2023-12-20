using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SVSTTest002Lib
{
    public class GAS_VALUESModel
    {
        [Key]
        public int GAS_VAL_ID { get; set; }

        public DateTime GAS_VAL_DATE { get; set; }
        public double H2_VAL { get; set; }
        public double O2_VAL { get; set; }

        public GAS_VALUESModel()
        {
        }

        public GAS_VALUESModel(DateTime timeStamp, double h2Value, double o2Value)
        {
            GAS_VAL_DATE = timeStamp;
            H2_VAL = h2Value;
            O2_VAL = o2Value;
        }
    }
}