using AutoMapper;
using Microsoft.Extensions.Logging;
using NewshoreAir.DataAccess.Repositories;
using NewshoreAir.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewshoreAir.Business.Services
{

    /// <summary>
    /// Servicio para gestionar los viajes.
    /// </summary>
    public class JourneyService : IJourneyService
    {
        private readonly IMapper _mapper;
        private readonly IFlightRepository _flightRepository;
        private readonly ILogger<JourneyService> _logger;

        public JourneyService(
            IFlightRepository flightRepository,
            IMapper mapper,
            ILogger<JourneyService> logger
            )
        {
            _flightRepository = flightRepository ?? throw new ArgumentNullException(nameof(flightRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Encuentra todas las rutas posibles desde el origen hasta el destino con un número máximo de paradas.
        /// </summary>
        /// <param name="origin">La estación de origen.</param>
        /// <param name="destination">La estación de destino.</param>
        /// <param name="maxStops">El número máximo de paradas.</param>
        /// <param name="currency">La moneda en la que se deben calcular los precios.</param>
        /// <returns>Una lista de todas las rutas posibles.</returns>
        public async Task<List<Journey>> FindRoutesWithStops(string origin, string destination, int maxStops, string currency)
        {
            try
            {
                var flightsAPI = await _flightRepository.GetAllFlights();
                var flights = flightsAPI.Select(flight => _mapper.Map<Flight>(flight)).ToList();
                var journeys = new List<Journey>();
                var currentRoute = new List<Flight>();
                FindRoutesRecursive(origin, destination, maxStops, currentRoute, journeys, flights);
                return UpdateJourney(journeys, origin, destination, currency);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al encontrar rutas con paradas");
                throw;
            }
        }

        /// <summary>
        /// Encuentra todas las rutas posibles desde el origen hasta el destino de forma recursiva.
        /// </summary>
        /// <param name="currentLocation">La ubicación actual.</param>
        /// <param name="destination">La estación de destino.</param>
        /// <param name="maxStops">El número máximo de paradas.</param>
        /// <param name="currentRoute">La ruta actual.</param>
        /// <param name="journeys">La lista de todos los viajes.</param>
        /// <param name="flightsAPI">La lista de todos los vuelos.</param>
        private void FindRoutesRecursive(
            string currentLocation,
            string destination,
            int maxStops,
            List<Flight> currentRoute,
            List<Journey> journeys,
            List<Flight> flightsAPI
        )
        {
            if (maxStops < 0)
            {
                return;
            }

            if (currentLocation == destination)
            {
                var newJourney = new Journey
                {
                    Flights = new List<Flight>(currentRoute)
                };
                journeys.Add(newJourney);
                return;
            }

            foreach (var flight in flightsAPI)
            {
                if (flight.Origin == currentLocation)
                {
                    currentRoute.Add(flight);
                    FindRoutesRecursive(flight.Destination, destination, maxStops - 1, currentRoute, journeys, flightsAPI);
                    currentRoute.Remove(flight);
                }
            }
        }

        /// <summary>
        /// Actualiza la información del viaje.
        /// </summary>
        /// <param name="journey">La lista de viajes.</param>
        /// <param name="origin">La estación de origen.</param>
        /// <param name="destination">La estación de destino.</param>
        /// <param name="currency">La moneda en la que se deben calcular los precios.</param>
        /// <returns>La lista de viajes actualizada.</returns>
        private List<Journey> UpdateJourney(List<Journey> journey, string origin, string destination, string currency)
        {
            foreach (var j in journey)
            {
                j.Origin = origin;
                j.Destination = destination;
                j.SetCurrency(currency);

            }
            return journey;
        }
    }
}
