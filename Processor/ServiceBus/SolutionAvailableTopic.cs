using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace QueensEight.Processor.ServiceBus
{
    public class SolutionAvailableTopic : ServiceBusTopic
    {
        public SolutionAvailableTopic(NamespaceManager namespaceManager, MessagingFactory messagingFactory) : base(namespaceManager, messagingFactory)
        {
        }

        protected override string TopicName
        {
            get { return "solutionavailable"; }
        }

        public void PublishSolution(Solution solution)
        {
            using (var message = new BrokeredMessage(solution))
            {
                SendMessage(message);
            }
        }
    }
}
