using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NewshoreAir.DataAccess.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewshoreAir.DataAccess.Repositories
{

    /// <summary>
    /// Repositorio para gestionar los vuelos.
    /// </summary>
    public class FlightRepository : IFlightRepository
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<FlightRepository> _logger;
        private readonly string _baseUrl;

        public FlightRepository(
            HttpClient httpClient,
            ILogger<FlightRepository> logger,
            IConfiguration configuration
            )
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _baseUrl = configuration["FlightAPI:BaseUrl"] ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Obtiene todos los vuelos.
        /// </summary>
        /// <returns>Una lista de todos los vuelos.</returns>
        public async Task<IEnumerable<FlightAPI>> GetAllFlights()
        {
            try
            {
                var response = await _httpClient.GetAsync(_baseUrl);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var flightsData = JsonConvert.DeserializeObject<IEnumerable<dynamic>>(content);

                    var flights = new List<FlightAPI>();

                    foreach (var flightData in flightsData!)
                    {
                        var flight = new FlightAPI
                        {
                            DepartureStation = flightData.departureStation,
                            ArrivalStation = flightData.arrivalStation,
                            Price = flightData.price,
                            Transport = new TransportAPI
                            {
                                FlightCarrier = flightData.flightCarrier,
                                FlightNumber = flightData.flightNumber
                            }
                        };

                        flights.Add(flight);
                    }

                    return flights;
                }
                else
                {
                    _logger.LogError($"Error al obtener los vuelos: {response.StatusCode}");
                    throw new Exception($"Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los vuelos");
                throw;
            }
        }
    }
}
