using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace tovuti_api.Models
{
    public class Parameters
    {
    }

    [DataContract]
    public class UserParameters
    {
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string username { get; set; }

    }

    [DataContract]
    public class Category
    {
        [DataMember]
        public string uid { get; set; }

        [DataMember]
        public string cname { get; set; }
        [DataMember]
        public string created_by { get; set; }

        [DataMember]
        public string modified_by { get; set; }

    }

    [DataContract]
    public class Product
    {
        [DataMember]
        public int pid { get; set; }
        [DataMember]
        public string uid { get; set; }

        [DataMember]
        public string pname { get; set; }

        [DataMember]
        public string cid { get; set; }

        [DataMember]
        public string created_by { get; set; }

        [DataMember]
        public string modified_by { get; set; }

        [DataMember]
        public bool has_attr { get; set; }

    }

    [DataContract]
    public class Attributes
    {
        [DataMember]
        public int attr_id { get; set; }
        [DataMember]
        public string pid { get; set; }
        [DataMember]
        public string pname { get; set; }

        [DataMember]
        public string color { get; set; }

        [DataMember]
        public string size { get; set; }

        [DataMember]
        public string created_by { get; set; }

        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public string gender { get; set; }

        [DataMember]
        public decimal price { get; set; }

    }

}