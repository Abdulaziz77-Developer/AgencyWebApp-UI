namespace TravelWebApp.Models
{
    public class AdminReviewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = "";
        public string Text{get;set; } = "";
        public int ItemId { get; set; }
        public string Item { get; set; } = "";
        public string Date { get; set; } = "";
        public string Price { get; set; } = "";
    }
}