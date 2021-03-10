using System.Runtime.Serialization;

namespace EventsLister.Service.SOAP
{
    [DataContract]
    public class FHTWEvent
    {
        [DataMember]
        public string Date { get; set; }
        [DataMember]
        public string Name { get; set; }
    }

}
