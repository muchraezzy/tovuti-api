using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace tovuti_api
{
    [DataContract]
    public class UserToken
    {

        [DataMember]
        public int uid { get; set; }
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Token { get; set; }
        [DataMember]
        public string CreateDate { get; set; }
    }

    [DataContract]
    public class Credentials
    {
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string IpAddress { get; set; }
    }

    [DataContract]
    public class Response
    {
        [DataMember]
        public string ResponseDetails { get; set; }
        [DataMember]
        public int ResponseCode { get; set; }
    }

}