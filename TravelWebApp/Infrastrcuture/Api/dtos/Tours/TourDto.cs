namespace TravelWebApp.Infrastrcuture.api.dtos.Tours
{
    public class TourDto
    {
        public int Id {get;set;}
        public string Title {get;set;} = "";
        public string Description {get;set;} = "";
        public decimal Price {get;set;}
        public string Region {get;set;} = "";
        public string PhotoUrl { get; set; } = "";
        public double Rating { get; set; }
        public int Duration { get; set; } 
        public bool Status { get; set;} 
        public bool IsAvailable {get;set;} = false;
        public double StartLatitude { get; set;}
        public double StartLongitude {get;set;}
   }
}