using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService
{
    // important WCF attributes
    // System.ServiceModel.ServiceContract
    //    means that interface defines a set of operations that external code can communicate with using SOAP.
    // System.ServiceModel.OperationContract
    //    means that this method inside the ServiceContract is an operation that is externally accessible
    // System.Runtime.Serialization.DataContract
    //    configure serialization for that type to be serialized/deserialized by DataContractSerializer
    //    (so it can be a parameter or return type of an operation method)
    // System.Runtime.Serialization.DataMember
    //    DataContractSerializer is opt-in for each member that will be serialized (unlike, e.g., System.Text.Json
    //     or Newtonsoft.Json in ASP.NET, with is opt-out with JsonIgnore)
    // FaultContract
    //   control serialization of server-side errors into SOAP faults.

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        //[FaultContract()]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
