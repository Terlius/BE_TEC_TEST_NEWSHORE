using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewshoreAir.Business.Models
{
    /// <summary>
    /// Representa un vuelo individual en un viaje.
    /// </summary>
    public class Flight
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public decimal Price { get; set; }
        public Transport Transport { get; set; }

        public string Currency { get; set; }

        private readonly Dictionary<string, decimal> exchangeRates;

        public Flight()
        {
            Origin = "N/A";
            Destination = "N/A";
            Price = 0;
            Transport = new Transport();
            Currency = "USD";
            exchangeRates = new Dictionary<string, decimal>
            {
                {"COP_TO_USD", 3800},
                {"EUR_TO_USD", 0.93m},
                {"COP_TO_EUR", 4380}
            };
        }

        /// <summary>
        /// Establece la moneda del vuelo y actualiza el precio.
        /// </summary>
        /// <param name="newCurrency">El tipo de moneda.</param>
        public void SetCurrency(string newCurrency)
        {
            if (string.IsNullOrEmpty(newCurrency))
            {
                throw new ArgumentException("El tipo de moneda no puede ser nulo o vacío.", nameof(newCurrency));
            }

            Price = ConverterPrice(Price, Currency, newCurrency);
            Currency = newCurrency;
        }

        /// <summary>
        /// Convierte el precio de un tipo de moneda a otro.
        /// </summary>
        /// <param name="price">El precio original.</param>
        /// <param name="currency">La moneda original.</param>
        /// <param name="newCurrency">La nueva moneda.</param>
        /// <returns>El precio convertido.</returns>
        public decimal ConverterPrice(decimal price, string currency, string newCurrency)
        {
            if (string.IsNullOrEmpty(currency) || string.IsNullOrEmpty(newCurrency))
            {
                throw new ArgumentException("La moneda original y la nueva moneda no pueden ser nulas o vacías.");
            }

            if (currency == newCurrency)
            {
                return price;
            }
            else if (newCurrency == "COP")
            {
                return price * exchangeRates["COP_TO_USD"];
            }
            else if (newCurrency == "EUR")
            {
                return price * exchangeRates["EUR_TO_USD"];
            }
            else
            {
                return price;
            }
        }
    }
}
