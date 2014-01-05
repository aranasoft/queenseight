using System;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace QueensEight.Processor.ServiceBus
{
    public class SolutionAvailableSubscription : ServiceBusSubscription
    {
        public SolutionAvailableSubscription(NamespaceManager namespaceManager, MessagingFactory messagingFactory, ServiceBusTopic topic, string subscriptionName) : base(namespaceManager, messagingFactory, topic, subscriptionName)
        {
        }

        public Solution Receive()
        {
            var message = Client.Receive(TimeSpan.FromSeconds(30));
            if (message == null) return null;

            var availableSolution = message.GetBody<Solution>();

            message.Complete();

            return availableSolution;
        }
    }
}