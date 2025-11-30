using Microsoft.Maui.Devices;

namespace EVCS.BlazorMauiApp.TriNM.Services
{
    public static class ApiConfig
    {
        // Thay đổi IP này thành IP của máy tính chạy server
        // Để lấy IP: chạy lệnh "ipconfig" trên Windows hoặc "ifconfig" trên Mac/Linux
        // Tìm IPv4 Address của adapter đang kết nối với cùng mạng WiFi với thiết bị Android
        private const string ServerIP = "10.1.163.112"; // Thay đổi IP này nếu cần
        private const int ServerPort = 7123;
        
        public static string GetBaseUrl()
        {
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                // Trên Android (thiết bị vật lý), sử dụng IP của máy tính
                return $"https://{ServerIP}:{ServerPort}";
            }
            else
            {
                // Trên emulator hoặc desktop, sử dụng localhost
                return $"https://localhost:{ServerPort}";
            }
        }
        
        public static string GetStationApiUrl()
        {
            return $"{GetBaseUrl()}/gateway/StationTriNM";
        }
        
        public static string GetChargerApiUrl()
        {
            return $"{GetBaseUrl()}/gateway/ChargerTriNM";
        }
    }
}

