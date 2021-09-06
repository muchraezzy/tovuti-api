using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using tovuti_api.Models;

namespace tovuti_api
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ITovuti
    {

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/register", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Response register(UserParameters body);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Authenticate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        UserToken Authenticate(Credentials creds);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Test", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string Test(string Param);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/AllUsers", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<User> AllUsers();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/productCategories", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<category> productCategories();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/productCategory", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Response productCategory(Category body);

        [OperationContract(Name ="postProducts")]
        [WebInvoke(Method = "POST", UriTemplate = "/products", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Response products(Product body);

        [OperationContract(Name = "getProducts")]
        [WebGet(UriTemplate = "/products?cid={cid}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<products> products(string cid);

        [OperationContract(Name = "postProductAttributes")]
        [WebInvoke(Method = "POST", UriTemplate = "/productAttributes", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Response productAttributes(Attributes body);

        [OperationContract(Name = "updateProductAttributes")]
        [WebInvoke(Method = "POST", UriTemplate = "/UpdateproductAttributes", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Response UpdateproductAttributes(Attributes body);

        [OperationContract(Name = "getProductAttributes")]
        [WebInvoke(Method = "GET", UriTemplate = "/productAttributes", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<attributes> productAttributes();

        [OperationContract(Name = "getproductAttributeValues")]
        [WebGet(UriTemplate = "/productAttributeValues?attrid={attr_id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<attributes> productAttributeValues(string attr_id);

        [OperationContract(Name = "putProducts")]
        [WebInvoke(Method = "PUT", UriTemplate = "/products", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Response EditProduct(Product body);



        //[OperationContract]
        //CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    //[DataContract]
    //public class CompositeType
    //{
    //    bool boolValue = true;
    //    string stringValue = "Hello ";

    //    [DataMember]
    //    public bool BoolValue
    //    {
    //        get { return boolValue; }
    //        set { boolValue = value; }
    //    }

        //[DataMember]
        //public string StringValue
        //{
        //    get { return stringValue; }
        //    set { stringValue = value; }
        //}
    //}
}
