using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace QueensEight.Processor.ServiceBus
{
    public abstract class ServiceBusTopic : ServiceBusContainer
    {
        protected ServiceBusTopic(NamespaceManager namespaceManager, MessagingFactory messagingFactory) : base(namespaceManager, messagingFactory)
        {
        }

        protected abstract string TopicName { get; }

        private TopicClient _topicClient;
        public TopicClient TopicClient
        {
            get
            {
                if (_topicClient != null) return _topicClient;

                _topicClient = _messagingFactory.CreateTopicClient(TopicName);

                return _topicClient;
            }
        }

        public bool Exists()
        {
            return _namespaceManager.TopicExists(TopicName);
        }

        public void CreateIfNotExists()
        {
            if (!Exists())
            {
                _namespaceManager.CreateTopic(TopicName);
            }
        }

        public void SendMessage(BrokeredMessage message)
        {
            TopicClient.Send(message);
        }

    }
}