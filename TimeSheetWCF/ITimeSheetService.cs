using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Collections.ObjectModel;

namespace TimeSheetWCF
{
    
    [ServiceContract]
    //[KnownType(typeof(Collection<string>))]
    public interface ITimeSheetService
    {

        [OperationContract]
        Collection<string> GetData(int value);
        [OperationContract]
        int UpdateData(int id, string name, string summary);
        [OperationContract]
        Collection<string> GetDetailData(int week, string username);
        [OperationContract]
        int SubmitData(int week, string name, string pic, string ps,
            string time, int pid, string text, double hours);
        [OperationContract]
        int UpdateDetailData(int week, string name, int pid, string text, double hours, int uid);
        [OperationContract]
        void DeleteDetailDate(int week, string name, int uid);
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
