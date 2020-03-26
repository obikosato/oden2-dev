using System.Runtime.Serialization;

namespace Common.Tools.WebSite
{
    [DataContract]
    public class EventInfo
    {
        [DataMember]
        public string date { get; set; }
        [DataMember]
        public string artist { get; set; }
        [DataMember]
        public string title { get; set; }
    }
}
