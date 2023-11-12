using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewshoreAir.DataAccess.Models
{
    public class FlightAPI
    {
        public TransportAPI Transport { get; set; } = new TransportAPI();
        public string DepartureStation { get; set; } = "";
        public string ArrivalStation { get; set; } = "";
        public decimal Price { get; set; } = 0;
    }
}
