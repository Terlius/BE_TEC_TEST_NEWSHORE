using NewshoreAir.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewshoreAir.Business.Services
{
    public interface IJourneyService
    {
        Task<List<Journey>> FindRoutesWithStops(string origin, string destination, int maxStops, string currency);
    }
}
