using EVCS.BusinessOjects.Shared.Models.trinm.DTOs;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelTriNM = EVCS.BusinessOjects.Shared.Models.trinm.Models;

namespace EVCS.ChargerTriNM.Microservices.TriNM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChargerTriNMController : ControllerBase
    {
        private readonly ILogger<ChargerTriNMController> _logger;
        private readonly IBus _bus;

        private readonly List<ModelTriNM.ChargerTriNM> _chargers;

        public ChargerTriNMController(ILogger<ChargerTriNMController> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;

            // Dữ liệu mẫu
            _chargers = new List<ModelTriNM.ChargerTriNM>
            {
                new ModelTriNM.ChargerTriNM
                {
                    ChargerTriNMId = 1,
                    StationTriNMId = 1,
                    ChargerTriNMType = "Type 2",
                    IsAvailable = true,
                    ImageURL = "https://example.com/charger1.jpg"
                },
                new ModelTriNM.ChargerTriNM
                {
                    ChargerTriNMId = 2,
                    StationTriNMId = 1,
                    ChargerTriNMType = "CCS",
                    IsAvailable = false,
                    ImageURL = "https://example.com/charger2.jpg"
                }
            };
        }

        // ===================== GET ALL =====================
        [HttpGet]
        public ActionResult<IEnumerable<ModelTriNM.ChargerTriNM>> GetAll()
        {
            return Ok(_chargers);
        }

        // ===================== GET BY ID =====================
        [HttpGet("{id}")]
        public ActionResult<ModelTriNM.ChargerTriNM> GetById(int id)
        {
            var item = _chargers.FirstOrDefault(x => x.ChargerTriNMId == id);
            if (item == null)
                return NotFound($"Không tìm thấy Charger có ID = {id}");

            return Ok(item);
        }

        // ===================== POST =====================
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ChargerTriNMDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data");

            // map sang model gốc
            var model = new ModelTriNM.ChargerTriNM
            {
                StationTriNMId = dto.StationTriNMId,
                ChargerTriNMType = dto.ChargerTriNMType ?? string.Empty,
                IsAvailable = dto.IsAvailable,
                ImageURL = dto.ImageURL ?? string.Empty
            };

            _logger.LogInformation($"[{DateTime.Now}] SUBMIT ChargerTriNM: {System.Text.Json.JsonSerializer.Serialize(model)}");

            Uri uri = new Uri("rabbitmq://localhost/chargerQueue");
            var endPoint = await _bus.GetSendEndpoint(uri);
            await endPoint.Send(model);

            return Ok(new
            {
                Message = "Đã gửi dữ liệu đến RabbitMQ thành công",
                Data = dto
            });
        }


        // ===================== PUT =====================
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ModelTriNM.ChargerTriNM model)
        {
            var existing = _chargers.FirstOrDefault(x => x.ChargerTriNMId == id);
            if (existing == null)
                return NotFound($"Không tìm thấy Charger có ID = {id}");

            existing.StationTriNMId = model.StationTriNMId;
            existing.ChargerTriNMType = model.ChargerTriNMType;
            existing.IsAvailable = model.IsAvailable;
            existing.ImageURL = model.ImageURL;

            _logger.LogInformation($"[{DateTime.Now}] Đã cập nhật Charger ID={id}");
            return Ok(existing);
        }

        // ===================== DELETE =====================
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _chargers.FirstOrDefault(x => x.ChargerTriNMId == id);
            if (item == null)
                return NotFound($"Không tìm thấy Charger có ID = {id}");

            _chargers.Remove(item);
            _logger.LogInformation($"[{DateTime.Now}] Đã xóa Charger ID={id}");
            return NoContent();
        }
    }
}

