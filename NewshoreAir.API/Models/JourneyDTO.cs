namespace NewshoreAir.API.Models
{
    public class JourneyDTO
    {
        public string Origin { get; set; } = "N/A";
        public string Destination { get; set; } = "N/A";
        public decimal Price { get; set; } = 0;
        public string CurrencyTemporal { get; set; } = "N/A";
        public List<FlightDTO> Flights { get; set; } = new List<FlightDTO>();



    }
}
