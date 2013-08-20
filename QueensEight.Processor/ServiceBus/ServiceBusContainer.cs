using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace QueensEight.Processor.ServiceBus
{
    public abstract class ServiceBusContainer
    {
        protected NamespaceManager _namespaceManager;
        protected MessagingFactory _messagingFactory;

        public ServiceBusContainer(NamespaceManager namespaceManager, MessagingFactory messagingFactory)
        {
            _namespaceManager = namespaceManager;
            _messagingFactory = messagingFactory;
        }
    }
}