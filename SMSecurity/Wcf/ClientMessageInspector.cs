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
    public class ClientMessageInspector : IClientMessageInspector
    {
        private AuthTokenHeader AuthToken;
        public ClientMessageInspector() : this(null)
        {

        }
        public ClientMessageInspector(AuthTokenHeader authToken)
        {
            this.AuthToken = authToken;
        }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            MessageHeader header = MessageHeader.CreateHeader(Constants.SEC_HEADER, Constants.SEC_NS, AuthToken);
            request.Headers.Add(header);
            return null;
        }
    }
}
