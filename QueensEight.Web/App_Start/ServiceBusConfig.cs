using QueensEight.Processor.ServiceBus;

namespace QueensEight.Web.App_Start
{
    public class ServiceBusConfig
    {
        public static void Setup()
        {
            ServiceBusUtilities.Setup();
        }
    }
}