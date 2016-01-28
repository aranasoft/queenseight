using System.Collections.Generic;
using System.Linq;
using Microsoft.ServiceBus.Messaging;
using QueensEight.Processor;

namespace QueensEight.Messaging.ServiceBus
{
    public class RequestedSolutions : ServiceBusQueue
    {
        protected override string QueueName => CustomConfiguration.QueueName;
        protected override string ConnectionString => CustomConfiguration.ConnectionString;

        public RequestedSolutions() {
            EnsureQueue();
        }

        public void Add(Solution partialSolution) {
            using (var message = new BrokeredMessage(partialSolution)) {
                SendMessage(message);
            }
        }

        public IEnumerable<Solution> All() {
            var pendingSolutions = PeekList().Select(message => message.GetBody<Solution>());
            return pendingSolutions;
        }
    }
}
