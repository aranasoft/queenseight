using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace QueensEight.Processor.ServiceBus
{
    public class ServiceBusUtilities
    {
        public static NamespaceManager NamespaceManager;
        public static MessagingFactory MessagingFactory;
        public static RequestedSolutions RequestedSolutionsQueue;
        public static SolutionAvailableTopic SolutionAvailableTopic;
        public static SolutionAvailableSubscription SolutionAvailableSubscription;

        public static void Setup()
        {
            var namespaceAddress = ServiceBusEnvironment.CreateServiceUri("sb", Configuration.Current.Namespace, string.Empty);

            var tokenProvider = TokenProvider.CreateSharedSecretTokenProvider(Configuration.Current.IssuerName, Configuration.Current.IssuerKey);
            NamespaceManager = new NamespaceManager(namespaceAddress, tokenProvider);
            MessagingFactory = MessagingFactory.Create(namespaceAddress, tokenProvider);

            RequestedSolutionsQueue = new RequestedSolutions(NamespaceManager, MessagingFactory);
            RequestedSolutionsQueue.CreateIfNotExists();

            SolutionAvailableTopic = new SolutionAvailableTopic(NamespaceManager,MessagingFactory);
            SolutionAvailableTopic.CreateIfNotExists();
        }

        public static void SetupSubscription(string subscriptionName)
        {
            SolutionAvailableSubscription = new SolutionAvailableSubscription(NamespaceManager,MessagingFactory,SolutionAvailableTopic,subscriptionName);
            SolutionAvailableSubscription.CreateIfNotExists();
        }
    }
}
