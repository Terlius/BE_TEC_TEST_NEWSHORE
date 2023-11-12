using NewshoreAir.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewshoreAir.DataAccess.Repositories
{
    public interface IFlightRepository
    {
        Task<IEnumerable<FlightAPI>> GetAllFlights();
    }
}
