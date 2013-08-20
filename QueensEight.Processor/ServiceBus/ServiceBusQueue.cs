using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace QueensEight.Processor.ServiceBus
{
    public abstract class ServiceBusQueue
    {
        protected abstract string QueueName { get; }

        protected NamespaceManager _namespaceManager;
        protected MessagingFactory _messagingFactory;

        public ServiceBusQueue(NamespaceManager namespaceManager, MessagingFactory messagingFactory)
        {
            _namespaceManager = namespaceManager;
            _messagingFactory = messagingFactory;
        }

        private QueueClient _queueClient;
        public QueueClient QueueClient
        {
            get
            {
                if (_queueClient != null) return _queueClient;

                _queueClient = _messagingFactory.CreateQueueClient(QueueName);

                return _queueClient;
            }
        }

        public bool Exists()
        {
            return _namespaceManager.QueueExists(QueueName);
        }

        public void CreateIfNotExists()
        {
            if (!Exists())
            {
                _namespaceManager.CreateQueue(QueueName);
            }
        }

        public void SendMessage(BrokeredMessage message)
        {
            QueueClient.Send(message);
        }
        
    }
}