using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFhotell
{
    [ServiceContract]
    public interface IHotelServices
    {
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{serviceType}")]
        List<Service> GetServices(string serviceType);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{serviceID}/{status}/{note}")]
        bool ChangeServiceStatus(string serviceID, string status, string note);
    }

    [DataContract]
    public class Service
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public String note { get; set; }
        [DataMember]
        public int roomId { get; set; }
        [DataMember]
        public int status { get; set; }
        [DataMember]
        public int type { get; set; }
    }
}
