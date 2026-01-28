# AgencyWebApp - Frontend (Blazor UI)

This is the client-side application for the **AgencyWebApp** ecosystem. It provides a modern, fast, and responsive web interface for customers and travel agents.

## 🚀 Key Features
- **Booking Management**: Browse and book tours, hotels, and flights.
- **Admin Dashboard**: Specialized interface for managing travel services and status tracking.
- **State Management**: Optimized data flow using Blazor's component architecture.
- **Real-time Updates**: Seamless integration with the Backend API.

## 🛠 Tech Stack
- **Framework**: Blazor Server
- **Styling**: Bootstrap 5 & Custom CSS
- **Communication**: HttpClient with JSON serialization
- **Icons**: FontAwesome and Bootstrap Icons

## ⚙️ How to Connect to Backend
Make sure to set the correct API URL in `Program.cs`:
```csharp
builder.Services.AddScoped(sp => new HttpClient 
{ 
    BaseAddress = new Uri("https://localhost:5001") // Your Backend URL
});