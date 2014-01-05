using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace QueensEight.Processor.ServiceBus
{
    public abstract class ServiceBusSubscription : ServiceBusContainer
    {
        protected ServiceBusTopic Topic { get; set; }
        protected ServiceBusSubscription(NamespaceManager namespaceManager, MessagingFactory messagingFactory, ServiceBusTopic topic, string subscriptionName) : base(namespaceManager, messagingFactory)
        {
            Topic = topic;
            SubscriptionName = subscriptionName;
        }

        public string SubscriptionName { get; set; }

        private SubscriptionClient _client;
        public SubscriptionClient Client
        {
            get
            {
                if (_client != null) return _client;

                _client = _messagingFactory.CreateSubscriptionClient(Topic.TopicClient.Path,SubscriptionName,ReceiveMode.PeekLock);

                return _client;
            }
        }



        public bool Exists()
        {
            return _namespaceManager.SubscriptionExists(Topic.TopicClient.Path, SubscriptionName);
        }

        public void CreateIfNotExists()
        {
            if (!Exists())
            {
                _namespaceManager.CreateSubscription(Topic.TopicClient.Path, SubscriptionName);
            }
        }


    }
}