using System.Collections.Generic;
using System.Net;
using Glimpse.Core.Extensibility;
using Glimpse.Core.Message;
using Glimpse.ExternalRequests.Models;

namespace Glimpse.ExternalRequests.AlternateTypes
{
    public class WebClientAlternateType : AlternateType<WebClient>
    {
        private IEnumerable<IAlternateMethod> allMethods;

        public WebClientAlternateType(IProxyFactory proxyFactory) : base(proxyFactory)
        {}

        public override IEnumerable<IAlternateMethod> AllMethods
        {
            get 
            { 
                return allMethods ?? (allMethods = new List<IAlternateMethod>
                {
                    new DownloadString(),
                    //new OnActionExecuted()
                }); 
            }
        }

        public class DownloadString : AlternateMethod
        {
            public DownloadString() : base(typeof(WebClient), "DownloadString")
            {
            }

            public override void PostImplementation(IAlternateMethodContext context, TimerResult timerResult)
            {
                //var actionContext = (ActionExecutingContext)context.Arguments[0];
                var message = new ExternalRequestMessage()
                    .AsTimelineMessage(new TimelineCategoryItem("External Request", "#f7325e", "#f7325e"));

                context.MessageBroker.Publish(message);
            }
        }
    }
}
