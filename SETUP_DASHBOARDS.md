# Dashboard & Environment Setup Guide

This guide details the configuration and execution steps for the Ocelot Gateway, RabbitMQ Dashboard, and Android environment.

## 1. Ocelot Gateway & Swagger UI

### Execution
1.  Navigate to the Gateway project directory:
    ```bash
    cd EVCS.OcelotAPIGateway.TriNM
    dotnet run
    ```
2.  Access Swagger UI: [https://localhost:7123/swagger](https://localhost:7123/swagger)

### API Endpoints
-   **Station API**: `https://localhost:7123/gateway/StationTriNM`
-   **Charger API**: `https://localhost:7123/gateway/ChargerTriNM`

> [!NOTE]
> A warning regarding the root path `/` is expected and does not affect functionality.

## 2. RabbitMQ Management Dashboard

### Configuration
Enable the management plugin via PowerShell (Administrator):

```powershell
# Navigate to RabbitMQ sbin directory (adjust version as needed)
cd "C:\Program Files\RabbitMQ Server\rabbitmq_server-4.1.4\sbin"

# Enable management plugin
.\rabbitmq-plugins enable rabbitmq_management

# Restart RabbitMQ Service
net stop RabbitMQ
net start RabbitMQ
```

### Access
-   **URL**: [http://localhost:15672](http://localhost:15672)
-   **Credentials**: `guest` / `guest`

### Verification
Check for the existence of `stationQueue` in the **Queues** tab to ensure the Station Microservice consumer is active.

## 3. Android Device Configuration (ADB)

### Host IP Configuration
The MAUI application is configured to connect to the host machine via a specific IP address when running on a physical Android device.

-   **Current Host IP**: `172.30.240.1`

### Verification
Verify connected devices:
```bash
adb devices
# Expected output: List of devices attached (e.g., R5CW20CBQFM)
```

### Updating Host IP
If your host IP changes:
1.  Retrieve new IP via `ipconfig`.
2.  Update `EVCS.BlazorMauiApp.TriNM\Components\Pages\Home.razor`:
    ```csharp
    var apiUrl = DeviceInfo.Platform == DevicePlatform.Android 
        ? "https://NEW_IP_ADDRESS:7123/gateway/StationTriNM" 
        : "https://localhost:7123/gateway/StationTriNM";
    ```

## 4. Service Execution Order

Run the following services in separate terminals:

### 1. Ocelot Gateway
```bash
cd EVCS.OcelotAPIGateway.TriNM
dotnet run
```
-   **Swagger**: `https://localhost:7123/swagger`

### 2. Station Microservice
```bash
cd EVCS.StationTriNM.Microservices.TriNM
dotnet run
```
-   **Swagger**: `https://localhost:7225/swagger`

### 3. Charger Microservice
```bash
cd EVCS.ChargerTriNM.Microservices.TriNM
dotnet run
```
-   **Swagger**: `https://localhost:7262/swagger`

### 4. MAUI App (Android)
```bash
cd EVCS.BlazorMauiApp.TriNM
dotnet build -f net8.0-android -t:Install
```

## 5. End-to-End Testing Flow

1.  Launch the MAUI app on the Android device.
2.  Submit the Station form.
3.  **Verify Logs**:
    -   **Gateway**: Check for request routing logs.
    -   **Station Service**: Look for `✅ POST StationTriNM received`.
    -   **Station Service (Consumer)**: Look for `✅ RECEIVE data from RabbitMQ.stationQueue`.
4.  **Verify RabbitMQ**:
    -   Check `stationQueue` at [http://localhost:15672](http://localhost:15672) for message activity.
