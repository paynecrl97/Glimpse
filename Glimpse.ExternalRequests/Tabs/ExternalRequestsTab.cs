using Glimpse.Core.Extensibility;
using Glimpse.Core.Extensions;
using Glimpse.ExternalRequests.Models;

namespace Glimpse.ExternalRequests.Tabs
{
    public class ExternalRequestsTab : TabBase, ITabSetup, IKey
    {
        public override string Name
        {
            get { return "External Requests"; }
        }

        public override object GetData(ITabContext context)
        {
            var actionFilterMessages = context.GetMessages<ExternalRequestMessage>();

            return actionFilterMessages;
        }

        public void Setup(ITabSetupContext context)
        {
            context.PersistMessages<ExternalRequestMessage>();
        }

        public string Key { get { return "glimpse_external_requests"; } }
    }
}
