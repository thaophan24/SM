using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace SM.Security.Wcf
{
    public class ServerMessageInspector : IDispatchMessageInspector
    {
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            int found = request.Headers.FindHeader("smheader", "ns");
            if (found != -1)
            {
                AuthTokenHeader authToken = request.Headers.GetHeader<AuthTokenHeader>("smheader", "ns");
                if (authToken == null || authToken.UserName == null || authToken.HashPassword == null)
                {
                    throw new FaultException("You might not have permission to access this operation.");
                }
            }
            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            
        }
    }
}
