using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace SMSecurity.WebService
{
    public class ClientBehaviorExtensionElement : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get
            {
                return typeof(ClientBehavior);
            }
        }

        protected override object CreateBehavior()
        {
            return new ClientBehavior();
        }
    }
}
