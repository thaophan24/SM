using SM.Security.Crypto;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            int found = request.Headers.FindHeader(Constants.SEC_HEADER, Constants.SEC_NS);
            if (found != -1)
            {
                AuthTokenHeader authToken = request.Headers.GetHeader<AuthTokenHeader>(Constants.SEC_HEADER, Constants.SEC_NS);
                if (authToken != null && authToken.UserName != null && authToken.HashPassword != null)
                {
                    if (!SecHelpers.IsAuthenticated(authToken.UserName, authToken.HashPassword))
                    {
                        throw new UnauthorizedAccessException("SM: You might not have permission to access this funtion.");
                    }
                }
            }
            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            
        }
    }
}
