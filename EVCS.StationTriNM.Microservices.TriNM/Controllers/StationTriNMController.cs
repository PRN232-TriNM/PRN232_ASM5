using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelTriNM = EVCS.BusinessOjects.Shared.Models.trinm.Models;

namespace EVCS.StationTriNM.Microservices.TriNM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationTriNMController : ControllerBase
    {
        private readonly ILogger<StationTriNMController> _logger;
        private readonly IBus _bus;
        private List<ModelTriNM.StationTriNM> Stations;

        public StationTriNMController(ILogger<StationTriNMController> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;
            // Tạo dữ liệu mẫu
            Stations = new List<ModelTriNM.StationTriNM>()
            {
                new ModelTriNM.StationTriNM()
                {
                    StationTriNMId = 1,
                    StationTriNMCode = "STN-001",
                    StationTriNMName = "Station A",
                    Address = "123 Main Street",
                    City = "Hanoi",
                    Province = "Hanoi",
                    Latitude = 21.0285m,
                    Longitude = 105.8542m,
                    Capacity = 10,
                    CurrentAvailable = 8,
                    Owner = "EVCS Company",
                    ContactPhone = "0123456789",
                    ContactEmail = "stationa@evcs.com",
                    Description = "Main charging station",
                    CreatedDate = DateTime.Now.AddYears(-1),
                    IsActive = true,
                    ImageURL = "https://example.com/station1.jpg"
                },
                new ModelTriNM.StationTriNM()
                {
                    StationTriNMId = 2,
                    StationTriNMCode = "STN-002",
                    StationTriNMName = "Station B",
                    Address = "456 Second Street",
                    City = "Ho Chi Minh",
                    Province = "Ho Chi Minh",
                    Latitude = 10.8231m,
                    Longitude = 106.6297m,
                    Capacity = 15,
                    CurrentAvailable = 12,
                    Owner = "EVCS Company",
                    ContactPhone = "0987654321",
                    ContactEmail = "stationb@evcs.com",
                    Description = "Secondary charging station",
                    CreatedDate = DateTime.Now.AddYears(-2),
                    IsActive = true,
                    ImageURL = "https://example.com/station2.jpg"
                }
            };
        }

        // GET: api/StationTriNM
        [HttpGet]
        public IEnumerable<ModelTriNM.StationTriNM> Get()
        {
            return Stations;
        }

        // GET api/StationTriNM/5
        [HttpGet("{id}")]
        public ActionResult<ModelTriNM.StationTriNM> Get(int id)
        {
            var item = Stations.Find(x => x.StationTriNMId == id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        // ===================== POST =====================
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ModelTriNM.StationTriNM station)
        {
            if (station == null)
            {
                _logger.LogWarning($"[{DateTime.Now}] POST StationTriNM: Invalid data (null)");
                return BadRequest("Invalid data");
            }

            // Tự động tạo ID nếu chưa có
            if (station.StationTriNMId == 0)
            {
                station.StationTriNMId = Stations.Any() ? (Stations.Max(x => x.StationTriNMId) + 1) : 1;
            }

            // Set CreatedDate nếu chưa có
            if (station.CreatedDate == default)
            {
                station.CreatedDate = DateTime.Now;
            }

            Stations.Add(station);

            _logger.LogInformation($"[{DateTime.Now}] ✅ POST StationTriNM received: {System.Text.Json.JsonSerializer.Serialize(station)}");

            // Gửi đến RabbitMQ
            try
            {
                Uri uri = new Uri("rabbitmq://localhost/stationQueue");
                var endPoint = await _bus.GetSendEndpoint(uri);
                await endPoint.Send(station);
                _logger.LogInformation($"[{DateTime.Now}] ✅ StationTriNM sent to RabbitMQ successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{DateTime.Now}] ❌ Error sending to RabbitMQ: {ex.Message}");
            }

            return Ok(new
            {
                Message = "Đã nhận và gửi dữ liệu StationTriNM đến RabbitMQ thành công",
                Data = station
            });
        }
    }
}
