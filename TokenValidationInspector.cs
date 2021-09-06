using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;

namespace tovuti_api
{
    public class TokenValidationInspector : IDispatchMessageInspector
    {
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            // Return BadRequest if request is null
            if (WebOperationContext.Current == null) { throw new WebFaultException(HttpStatusCode.BadRequest); }

            //exemptions
            if (request.Headers.To.LocalPath == "/tovuti_api.svc")
                return null;
            if (request.Headers.To.LocalPath == "/tovuti_api.svc/Authenticate")
                return null;
            if (request.Headers.To.LocalPath == "/tovuti_api.svc/register")
                return null;
            //if (request.Headers.To.LocalPath == "/tovuti_api.svc/ResetPassword")
            //    return null;
            //if (request.Headers.To.LocalPath == "/tovuti_api.svc/ChangePassword")
            //    return null;

            // Get Token from header
            var token = WebOperationContext.Current.IncomingRequest.Headers["Token"];

            // Validate the Token
            using (var dbContext = new ApplicationDbContext("public"))
            {
                ITokenValidator validator = new TokenValidator(dbContext);
                if (!validator.IsValid(token))
                {

                    throw new WebFaultException<string>("Unauthorized: Session Expired", HttpStatusCode.Unauthorized);
                }

            }
            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
        }
    }
}