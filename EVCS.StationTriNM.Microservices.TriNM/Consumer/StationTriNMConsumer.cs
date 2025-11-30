using EVCS.Common.Shared.trinm;
using MassTransit;
using ModelTriNM = EVCS.BusinessOjects.Shared.Models.trinm.Models;

namespace EVCS.StationTriNM.Microservices.TriNM.Consumer
{
    public class StationTriNMConsumer : IConsumer<ModelTriNM.StationTriNM>
    {
        private readonly ILogger<StationTriNMConsumer> _logger;

        public StationTriNMConsumer(ILogger<StationTriNMConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<ModelTriNM.StationTriNM> context)
        {
            var data = context.Message;


            if (data != null)
            {
                string jsonData = Utilities.ConvertObjectToJSONString(data);
                string messageLog = $"[{DateTime.Now}] RECEIVE data from RabbitMQ.stationQueue: {jsonData}";

                Utilities.WriteLoggerFile(messageLog);

                _logger.LogInformation("[{Time}] RECEIVE data from RabbitMQ.stationQueue: {Data}", DateTime.Now, jsonData);
            }
            else
            {
                _logger.LogWarning("Received null data from RabbitMQ.stationQueue");
            }
        }
    }
}

