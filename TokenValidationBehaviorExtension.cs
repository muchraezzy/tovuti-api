using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Configuration;

namespace tovuti_api
{
    public class TokenValidationBehaviorExtension : BehaviorExtensionElement
    {
        #region BehaviorExtensionElement

        public override Type BehaviorType
        {
            get { return typeof(TokenValidationServiceBehavior); }
        }

        protected override object CreateBehavior()
        {
            return new TokenValidationServiceBehavior();
        }

        #endregion
    }
}