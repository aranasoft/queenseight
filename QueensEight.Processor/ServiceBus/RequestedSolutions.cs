using System;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace QueensEight.Processor.ServiceBus
{
    public class RequestedSolutions : ServiceBusQueue
    {
        public RequestedSolutions(NamespaceManager namespaceManager, MessagingFactory messagingFactory) : base(namespaceManager, messagingFactory)
        {
        }

        protected override string QueueName
        {
            get { return "requestedsolutions"; }
        }

        public void RequestSolution(Solution partialSolution)
        {
            using (var message = new BrokeredMessage(partialSolution))
            {
                SendMessage(message);
            }
        }

        public Solution FetchSolutionRequest(TimeSpan serverWaitTime )
        {
            var message = QueueClient.Receive(serverWaitTime);

            if (message == null) return null;

            var solutionRequest = message.GetBody<Solution>();

            message.Complete();

            return solutionRequest;
        }

    }
}