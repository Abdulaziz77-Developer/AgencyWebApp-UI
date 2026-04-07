using System.Text.Json.Serialization;

namespace TravelWebApp.Infrastrcuture.api.dtos.Users
{
    public class UserDto
    {
        public int Id { get; set; }
        [JsonPropertyName("fullName")]
        public string FullName { get; set; } = "";
        [JsonPropertyName("email")]
        public string Email { get; set; } = "";
        public int Role { get; set; }
    }
}