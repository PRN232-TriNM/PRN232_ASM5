# EV Charging Station (EVCS) Management System

A comprehensive microservices-based solution for managing Electric Vehicle Charging Stations, featuring a cross-platform mobile application and a robust backend architecture.

## 🚀 Tech Stack

### Core Frameworks
- **.NET 8**: The latest long-term support (LTS) release of the .NET platform.
- **ASP.NET Core Web API**: High-performance APIs for microservices.
- **.NET MAUI (Blazor Hybrid)**: Cross-platform mobile application (Android, iOS, Windows, macOS) leveraging web technologies.

### Architecture & Communication
- **Microservices Architecture**: Decoupled services for scalability and maintainability.
- **Ocelot**: API Gateway for unified entry point and routing.
- **RabbitMQ**: Message broker for asynchronous service-to-service communication.
- **MassTransit**: Distributed application framework for .NET, simplifying message bus integration.

### Documentation & Tools
- **Swagger / OpenAPI**: Interactive API documentation.
- **Visual Studio 2022**: Primary IDE.

## 📂 Project Structure

| Project | Description |
|---------|-------------|
| **EVCS.OcelotAPIGateway.TriNM** | API Gateway routing requests to appropriate microservices. |
| **EVCS.StationTriNM.Microservices.TriNM** | Microservice handling Station-related operations. |
| **EVCS.ChargerTriNM.Microservices.TriNM** | Microservice handling Charger-related operations. |
| **EVCS.BlazorMauiApp.TriNM** | Client-facing mobile application built with MAUI Blazor. |
| **EVCS.BusinessOjects.Shared.Models.TriNM** | Shared data models and DTOs. |
| **EVCS.Common.Shared.TriNM** | Common utilities and helper classes. |

## 🔧 Getting Started

### Prerequisites
- .NET 8 SDK
- RabbitMQ Server (Running locally or via Docker)
- Android Emulator or Physical Device (for MAUI App)

### Installation & Run

1.  **Start RabbitMQ**: Ensure your RabbitMQ instance is running.
2.  **Run Microservices**:
    ```bash
    dotnet run --project EVCS.StationTriNM.Microservices.TriNM
    dotnet run --project EVCS.ChargerTriNM.Microservices.TriNM
    ```
3.  **Run API Gateway**:
    ```bash
    dotnet run --project EVCS.OcelotAPIGateway.TriNM
    ```
4.  **Run Mobile App**:
    ```bash
    dotnet run --project EVCS.BlazorMauiApp.TriNM -f net8.0-android
    ```

## 🔗 API Documentation
Once services are running, access Swagger UI at:
- Gateway: `https://localhost:7123/swagger`
- Station Service: `https://localhost:7225/swagger`
- Charger Service: `https://localhost:7262/swagger`
