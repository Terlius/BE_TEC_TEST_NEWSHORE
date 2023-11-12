namespace NewshoreAir.API.Models
{

    public class FlightDTO
    {
        public string Origin { get; set; } = "N/A";
        public string Destination { get; set; } = "N/A";
        public decimal Price { get; set; } = 0;
        public TransportDTO Transport { get; set; } = new TransportDTO();
        public string Currency { get; set; } = "N/A";


    }
}
