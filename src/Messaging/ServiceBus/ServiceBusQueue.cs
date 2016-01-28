using System.Collections.Generic;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace QueensEight.Messaging.ServiceBus {
    public abstract class ServiceBusQueue {
        protected abstract string QueueName { get; }
        protected abstract string ConnectionString { get; }

        protected void EnsureQueue() {
            if (!NamespaceManager.QueueExists(QueueName)) {
                NamespaceManager.CreateQueue(QueueName);
            }
        }


        private NamespaceManager _namespaceManager = null;
        protected NamespaceManager NamespaceManager {
            get {
                _namespaceManager = _namespaceManager ?? (_namespaceManager = NamespaceManager.CreateFromConnectionString(ConnectionString));
                return _namespaceManager;
            }
        }

        private QueueClient _queueClient = null;

        protected QueueClient QueueClient {
            get {
                _queueClient = _queueClient ?? (_queueClient = QueueClient.CreateFromConnectionString(ConnectionString));
                return _queueClient;
            }
        }
        public void SendMessage(BrokeredMessage message)
        {
            QueueClient.Send(message);
        }

        public IEnumerable<BrokeredMessage> PeekList() {
            //TODO: deal with more than 10 items
            var pendingMessages = QueueClient.PeekBatch(10);

            return pendingMessages;
        }
    }
}