using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewshoreAir.Business.Models
{
    /// <summary>
    /// Representa un viaje que consiste en una serie de vuelos.
    /// </summary>
    public class Journey
    {
        public string Origin { get; set; } = "N/A";
        public string Destination { get; set; } = "N/A";
        public decimal Price { get; set; } = 0;
        public string Currency { get; set; } = "USD";
        public List<Flight> Flights { get; set; } = new List<Flight>();

        /// <summary>
        /// Actualiza el precio del viaje sumando los precios de todos los vuelos.
        /// </summary>
        public void UpdatePrice()
        {
            if (Flights == null || !Flights.Any())
            {
                throw new InvalidOperationException("No hay vuelos en este viaje.");
            }

            Price = Flights.Sum(flight => flight.Price);
        }

        /// <summary>
        /// Establece la moneda del viaje y actualiza los precios de los vuelos.
        /// </summary>
        /// <param name="type">El tipo de moneda.</param>
        public void SetCurrency(string type)
        {
            if (string.IsNullOrEmpty(type))
            {
                throw new ArgumentException("El tipo de moneda no puede ser nulo o vacío.", nameof(type));
            }

            if (Flights == null || !Flights.Any())
            {
                throw new InvalidOperationException("No hay vuelos en este viaje.");
            }

            foreach (var flight in Flights)
            {
                flight.SetCurrency(type);
            }

            Currency = type;
            UpdatePrice();
        }
    }
}
