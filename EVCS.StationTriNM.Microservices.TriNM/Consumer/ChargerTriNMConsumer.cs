using EVCS.BusinessOjects.Shared.Models.trinm.Models;
using EVCS.Common.Shared.trinm;
using MassTransit;

namespace EVCS.StationTriNM.Microservices.TriNM.Consumer
{
    public class ChargerTriNMConsumer : IConsumer<ChargerTriNM>
    {
        private readonly ILogger<ChargerTriNMConsumer> _logger;

        public ChargerTriNMConsumer(ILogger<ChargerTriNMConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<ChargerTriNM> context)
        {
            var data = context.Message;


            if (data != null)
            {
                string messageLog = string.Format("[{0}] RECEIVE data from RabbitMQ.chargerQueue: {1}", DateTime.Now.ToString(), Utilities.ConvertObjectToJSONString(data));


                Utilities.WriteLoggerFile(messageLog);

                _logger.LogInformation(messageLog);
            }
            else
            {
                _logger.LogWarning("Received null data from RabbitMQ.chargerQueue");
            }
        }
    }
}

