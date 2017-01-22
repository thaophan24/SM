using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace SMSecurity.WebService
{
    public class ClientBehavior : IEndpointBehavior
    {
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {

        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            ClientCredentials cred = null;
            foreach (IEndpointBehavior item in endpoint.EndpointBehaviors)
            {
                if (item is ClientCredentials)
                {
                    cred = (ClientCredentials)item;
                    break;
                }
            }
            AuthTokenHeader authToken = null;
            if (cred != null)
            {
                authToken = new AuthTokenHeader()
                {
                    UserName = cred.UserName.UserName,
                    HashPassword = cred.UserName.Password
                };
            }
            clientRuntime.MessageInspectors.Add(new ClientMessageInspector(authToken));
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }
    }
}
