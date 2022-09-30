using System.Runtime.Serialization;

namespace WO.Hub.Contract;

[DataContract]
public class ApplyRequest
{
    [DataMember(Order = 1)]
    public string Method { get; set; }
}
