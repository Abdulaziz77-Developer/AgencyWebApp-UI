namespace TravelWebApp.Infrastrcuture.api.dtos.Hotels
{
    public class HotelDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public decimal Price { get; set; }
        public string City { get; set; } = "";
        public string PhotoUrl { get; set; } = "";
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Rating { get; set; }
        public bool IsAvailable { get; set; } = false;
        public bool Status { get; set; }
    }
}